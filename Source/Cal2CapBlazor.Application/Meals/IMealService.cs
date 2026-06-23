using Cal2CapBlazor.Application.Meals.DataTransferObjects;

namespace Cal2CapBlazor.Application.Meals;
public interface IMealService {
    Task CreateMealAsync(Guid accountId, MealDTO mealDTO);
    Task ChangeMealAsync(MealDTO mealDTO);
    Task DeleteMealAsync(DeleteMealDTO deleteDTO);
}