namespace Cal2CapBlazor.Application.Meals.DataTransferObjects;

public record MealItemDto(
    Guid Id,
    string MealName,
    string MealDetails,
    int MealType, 
    DateTime InTakeTime,
    int? Calorie,
    int? Carbohydrate,
    int? Protein,
    int? Fat,
    int? Calcium,
    int? Iron,
    int? Magnesium,
    int? Zinc,
    int? VitaminA,
    int? VitaminB,
    int? VitaminC,
    int? VitaminD,
    int? VitaminE
);