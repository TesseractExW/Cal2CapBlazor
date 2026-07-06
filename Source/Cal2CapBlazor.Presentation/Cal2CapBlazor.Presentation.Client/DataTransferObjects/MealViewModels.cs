namespace Cal2CapBlazor.Presentation.Client.DataTransferObjects;
public class CreateMealViewModel
{
    public string MealName { get; set; } = string.Empty;
    public string MealDetails { get; set; } = string.Empty;
    public int MealType { get; set; } = 1;
    public DateTime InTakeTime { get; set; } = DateTime.UtcNow;
}

public class MealItemViewModel
{
    public Guid Id { get; set; }
    public string MealName { get; set; } = string.Empty;
    public string MealDetails { get; set; } = string.Empty;
    public int MealType { get; set; }
    public DateTime InTakeTime { get; set; }
    public int? Calorie { get; set; }
    public int? Carbohydrate { get; set; }
    public int? Protein { get; set; }
    public int? Fat { get; set; }
    public int? Calcium { get; set; }
    public int? Iron { get; set; }
    public int? Magnesium { get; set; }
    public int? Zinc { get; set; }
    public int? VitaminA { get; set; }
    public int? VitaminB { get; set; }
    public int? VitaminC { get; set; }
    public int? VitaminD { get; set; }
    public int? VitaminE { get; set; }
}

public class ChangeMealDetailsViewModel
{
    public Guid Id { get; set; }
    public string MealDetails { get; set; } = string.Empty;
}

public class ChangeMealNameViewModel
{
    public Guid Id { get; set; }
    public string MealName { get; set; } = string.Empty;
}

public class ChangeMealTypeViewModel
{
    public Guid Id { get; set; }
    public int MealType { get; set; }
}

public class ChangeMealNutrientsViewModel
{
    public Guid Id { get; set; }
    public string MealName { get; set; } = string.Empty;
    public string MealDetails { get; set; } = string.Empty;
    public int MealType { get; set; }
    public DateTime InTakeTime { get; set; }
    public int? Calorie { get; set; }
    public int? Carbohydrate { get; set; }
    public int? Protein { get; set; }
    public int? Fat { get; set; }
    public int? Calcium { get; set; }
    public int? Iron { get; set; }
    public int? Magnesium { get; set; }
    public int? Zinc { get; set; }
    public int? VitaminA { get; set; }
    public int? VitaminB { get; set; }
    public int? VitaminC { get; set; }
    public int? VitaminD { get; set; }
    public int? VitaminE { get; set; }
}

public class PagedMealsViewModel
{
    public List<MealItemViewModel> Items { get; set; } = new();
    public int TotalPage { get; set; }
    public int TotalCount { get; set; }
}