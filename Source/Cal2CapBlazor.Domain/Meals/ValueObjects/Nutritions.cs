namespace Cal2CapBlazor.Domain.Meals.ValueObjects;

public record Nutritions(
    // Macronutritions
    int? Calorie,
    int? Carbohydrate,
    int? Protein,
    int? Fat,

    // Minerals
    int? Calcium,
    int? Iron,
    int? Magnesium,
    int? Zinc,

    // Vitamins
    int? VitaminA,
    int? VitaminB,
    int? VitaminC,
    int? VitaminD,
    int? VitaminE) 
{
    public static readonly Nutritions None = new Nutritions(
        null, null, null, null, 
        null, null, null, null,
        null, null, null, null, null
    );
}