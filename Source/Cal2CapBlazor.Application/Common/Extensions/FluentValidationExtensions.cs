using Cal2CapBlazor.Domain.Common;
using FluentValidation;

namespace Cal2CapBlazor.Application.Common.Extensions;
public static class FluentValidationExtensions
{
    public static void MustBeValueObject<T, TProperty, TValueObject>(
        this IRuleBuilder<T, TProperty> ruleBuilder,
        Func<TProperty, Result<TValueObject>> createMethod)
        where TValueObject : notnull
    {
        ruleBuilder.Custom((value, context) =>
        {
            var result = createMethod(value);
            if (!result.IsSuccess)
            {
                context.AddFailure(result.Error.Id, result.Error.Message);
            }
        });
    }
}