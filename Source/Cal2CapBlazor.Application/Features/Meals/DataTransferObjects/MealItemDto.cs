namespace Cal2CapBlazor.Application.Meals.DataTransferObjects;

public record MealItemDto(
    string MealName,
    string MealDetails,
    int MealType, 
    DateTime InTakeTime,
    // Macronutrients
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
    int? VitaminE
);