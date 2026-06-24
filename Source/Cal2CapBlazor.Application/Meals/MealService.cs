using Cal2CapBlazor.Domain.Meals;
using Cal2CapBlazor.Application.Common.Exceptions;
using Cal2CapBlazor.Application.Meals.DataTransferObjects;
using Cal2CapBlazor.Application.Authentications;

namespace Cal2CapBlazor.Application.Meals;

public class MealService
{
    protected IMealRepository repository { get; set; }
    protected IAuthentication auth       { get; set; }

    public MealService(IMealRepository mealRepository, IAuthentication authentication)
    {
        repository = mealRepository;
        auth = authentication;
    }

    #region Private Helpers
    protected async Task<MealEntity> GetMealAsync(Guid id)
    {
        if (!auth.IsAuthenticated)
        {
            throw new UnauthenticatedException();
        }
        MealEntity? meal = await repository.GetByIdAsync(id);
        if (meal is null)
        {
            throw new NotFoundException("The provided meal cannot be found in database.");
        }
        else if (meal.AccountId != auth.AccountId)
        {
            throw new UnauthorizedExeption();
        }
        return meal;
    }
    #endregion

    #region Service Methods
    public async Task CreateMealAsync(CreateMealDTO createMealDTO)
    {
        if (!auth.IsAuthenticated)
        {
            throw new UnauthenticatedException();
        }
        MealEntity meal = new MealEntity(auth.AccountId)
        {
            Name            = createMealDTO.Name,
            InTakeTime      = createMealDTO.InTakeTime,
            Calorie         = createMealDTO.Calorie,
            Carbohydrate    = createMealDTO.Carbohydrate,
            Protein         = createMealDTO.Protein,
            Fat             = createMealDTO.Fat,
            Calcium         = createMealDTO.Calcium,
            Iron            = createMealDTO.Iron,
            Magnesium       = createMealDTO.Magnesium,
            Zinc            = createMealDTO.Zinc,
            VitaminA        = createMealDTO.VitaminA,
            VitaminB        = createMealDTO.VitaminB,
            VitaminC        = createMealDTO.VitaminC,
            VitaminD        = createMealDTO.VitaminD,
            VitaminE        = createMealDTO.VitaminE
        };

        await repository.AddMealAsync(meal);
    }

    public async Task ChangeMealAsync(ChangeMealDTO changeMealDTO)
    {
        MealEntity meal = await GetMealAsync(changeMealDTO.Id);

        meal.Name           = changeMealDTO.Name;
        meal.InTakeTime     = changeMealDTO.InTakeTime;
        meal.Calorie        = changeMealDTO.Calorie;
        meal.Carbohydrate   = changeMealDTO.Carbohydrate;
        meal.Protein        = changeMealDTO.Protein;
        meal.Fat            = changeMealDTO.Fat;
        meal.Calcium        = changeMealDTO.Calcium;
        meal.Iron           = changeMealDTO.Iron;
        meal.Magnesium      = changeMealDTO.Magnesium;
        meal.Zinc           = changeMealDTO.Zinc;
        meal.VitaminA       = changeMealDTO.VitaminA;
        meal.VitaminB       = changeMealDTO.VitaminB;
        meal.VitaminC       = changeMealDTO.VitaminC;
        meal.VitaminD       = changeMealDTO.VitaminD;
        meal.VitaminE       = changeMealDTO.VitaminE;

        await repository.UpdateMealAsync(meal);
    }

    public async Task DeleteMealAsync(DeleteMealDTO deleteMealDTO)
    {
        MealEntity meal = await GetMealAsync(deleteMealDTO.Id);

        await repository.DeleteMealAsync(meal);
    }
    #endregion
}