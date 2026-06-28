namespace Cal2CapBlazor.Domain.Meals.ValueObjects;

[Flags]
public enum MealType
{
    None        = 0,
    Breakfast   = 1 << 0,
    Lunch       = 1 << 1,
    Dinner      = 1 << 2,
    LateNight   = 1 << 3,
    Snack       = 1 << 4,
    Break       = 1 << 5,
    Clean       = 1 << 6,
    Heavy       = 1 << 7,
}