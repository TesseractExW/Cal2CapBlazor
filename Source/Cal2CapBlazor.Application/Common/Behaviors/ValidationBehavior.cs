using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;

namespace Cal2CapBlazor.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest  : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        ValidationContext<TRequest> context  = new ValidationContext<TRequest>(request);
        ValidationResult[] validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken))
        );

        IEnumerable<ValidationFailure> validationFailures = validationResults
            .SelectMany(v => v.Errors)
            .Where(e => e is not null)
            .ToList();

        if (validationFailures.Any())
        {
            ValidationFailure firstFailure = validationFailures.First();
            ErrorResult error = new ErrorResult(
                $"{typeof(TRequest).Name},ValidationError",
                firstFailure.ErrorMessage
            );

            // In case of TReponse = Result<T>
            if (typeof(TResponse).IsGenericType)
            {
                MethodInfo failureMethod = typeof(Result<>)
                    .MakeGenericType(typeof(TResponse).GetGenericArguments()[0])
                    .GetMethod("Failure", [typeof(ErrorResult)])!;

                return (TResponse)failureMethod!.Invoke(null, [error])!;
            }

            return (TResponse)Result.Failure(error);
        }

        return await next();
    }
}