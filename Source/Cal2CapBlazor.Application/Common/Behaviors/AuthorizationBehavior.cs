using System.Reflection;
using MediatR;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Application.Common.Security;

namespace Cal2CapBlazor.Application.Common.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse>(
    ICurrentUserService currentUser)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        Type requestType = request.GetType();
        IEnumerable<RequireRoleAttribute> attributes = request.GetType().GetCustomAttributes<RequireRoleAttribute>();

        if (!attributes.Any())
        {
            if (requestType.GetCustomAttribute<GuestOnlyAttribute>() is not null &&
                currentUser.IsAuthenticated)
            {
                return CreateFailureResult(new ErrorResult("Auth.GuestOnly", "you are already logged in."));
            }
            return await next();
        }

        if (!currentUser.IsAuthenticated)
        {
            return CreateFailureResult(new ErrorResult("Auth.Unauthenticated", "You must be logged in."));
        }

        foreach (RequireRoleAttribute attr in attributes)
        {
            if (!currentUser.HasRole(attr.Role))
            {
                return CreateFailureResult(new ErrorResult("Auth.Unauthorized", "You do not have permission to perform this action."));
            }
        }

        return await next();
    }

    private static TResponse CreateFailureResult(ErrorResult error)
    {
        if (typeof(TResponse) == typeof(Result))
        {
            return (TResponse)Result.Failure(error);
        }

        if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            Type genericUnderlyingType = typeof(TResponse).GetGenericArguments()[0];
            
            MethodInfo? failureMethod = typeof(Result<>)
                .MakeGenericType(genericUnderlyingType)
                .GetMethod("Failure", BindingFlags.Public | BindingFlags.Static);

            object? failureResult = failureMethod?.Invoke(null, [error]);
            return (TResponse)failureResult!;
        }

        throw new InvalidOperationException($"The response type {typeof(TResponse).Name} is not supported by the authorization pipeline.");
    }
}