using Cal2CapBlazor.Application.Meals.DataTransferObjects;

namespace Cal2CapBlazor.Application.Meals;

public interface IMealService 
{
    Task CreateMealAsync(CreateMealDTO createMealDTO);

    Task ChangeMealAsync(ChangeMealDTO changeMealDTO);

    Task DeleteMealAsync(DeleteMealDTO deleteMealDTO);
}