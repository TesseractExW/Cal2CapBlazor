namespace Cal2CapBlazor.Domain.Meals.ValueObjects;
public record NutrientProfile(
    Calorie Calorie,
    Weight Carbohydrate,
    Weight Protein,
    Weight Fat,
    Weight Calcium,
    Weight Iron,
    Weight Magnesium,
    Weight Zinc,
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