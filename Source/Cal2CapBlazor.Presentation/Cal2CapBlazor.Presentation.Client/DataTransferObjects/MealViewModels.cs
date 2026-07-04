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
    // ... add other macros/vitamins if needed for the UI
}

public class PagedMealsViewModel
{
    public List<MealItemViewModel> Items { get; set; } = new();
    public int TotalPage { get; set; }
    public int TotalCount { get; set; }
}