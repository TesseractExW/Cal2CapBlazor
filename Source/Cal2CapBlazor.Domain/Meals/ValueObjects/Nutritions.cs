namespace Cal2CapBlazor.Domain.Meals.ValueObjects;

public record Nutritions(
    // Macronutritions
    int? Calorie        = null,
    int? Carbohydrate   = null,
    int? Protein        = null,
    int? Fat            = null,
    // Minerals
    int? Calcium        = null,
    int? Iron           = null,
    int? Magnesium      = null,
    int? Zinc           = null,
    // Vitamins
    int? VitaminA       = null,
    int? VitaminB       = null,
    int? VitaminC       = null,
    int? VitaminD       = null,
    int? VitaminE       = null) 
{
    public static readonly Nutritions None = new Nutritions();
}