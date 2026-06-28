using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;

namespace Cal2CapBlazor.Domain.Meals.ValueObjects;

public record Unit(int? Value = null);

public record Weight(int? Value = null) : Unit(Value)
{
    public static Result<Weight> Create(int? value)
    {
        if (value is not null && value < 0)
        {
            return Result<Weight>.Failure(new NegativeError("Gram", "weight"));
        }

        return Result<Weight>.Success(new Weight(value));
    }
}

public record Calorie(int? Value = null) : Unit(Value)
{
    public static Result<Calorie> Create(int? value)
    {
        if (value is not null && value < 0)
        {
            return Result<Calorie>.Failure(new NegativeError("Calorie", "energy"));
        }

        return Result<Calorie>.Success(new Calorie(value));
    }
}