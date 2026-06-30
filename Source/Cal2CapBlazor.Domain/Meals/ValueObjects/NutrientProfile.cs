namespace Cal2CapBlazor.Domain.Meals.ValueObjects;

public record NutrientProfile(
    // Macronutritions
    Calorie Calorie,
    Weight Carbohydrate,
    Weight Protein,
    Weight Fat,
    // Minerals
    Weight Calcium,
    Weight Iron,
    Weight Magnesium,
    Weight Zinc,
    // Vitamins
    Weight VitaminA,
    Weight VitaminB,
    Weight VitaminC,
    Weight VitaminD,
    Weight VitaminE)
{
    public static readonly NutrientProfile None = new NutrientProfile(
        new Calorie(null),
        new Weight(null),
        new Weight(null),
        new Weight(null),
        new Weight(null),
        new Weight(null),
        new Weight(null),
        new Weight(null),
        new Weight(null),
        new Weight(null),
        new Weight(null),
        new Weight(null),
        new Weight(null)
    );
}