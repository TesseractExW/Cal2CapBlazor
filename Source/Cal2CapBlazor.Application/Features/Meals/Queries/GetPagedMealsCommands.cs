using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Application.Meals.DataTransferObjects;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Meals;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cal2CapBlazor.Application.Meals.Queries;
public record PagedMealsQueryReponse(
    IReadOnlyCollection<MealItemDto> Items,
    int TotalPage,
    int TotalCount);

public record GetPagedMealsQuery(
    int PageIndex, 
    int PageSize, 
    string? SearchTerm) 
    : IRequest<Result<PagedMealsQueryReponse>>;

internal sealed class GetPagedMealsQueryHandler(
    IApplicationDbContext dbContext,
    IUserContext userContext)
    : IRequestHandler<GetPagedMealsQuery, Result<PagedMealsQueryReponse>>
{
    public async Task<Result<PagedMealsQueryReponse>> Handle(
        GetPagedMealsQuery query, 
        CancellationToken cancellationToken)
    {
        int pageIndex = Math.Max(1, query.PageIndex);
        int pageSize = Math.Clamp(query.PageSize, 1, 10);

        IQueryable<Meal> mealQuery  = dbContext.Meals
            .AsNoTracking()
            .Where(e => e.AccountId == userContext.Id);

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            mealQuery = mealQuery.Where(e => e.MealName.Value.Contains(query.SearchTerm));
        }

        int totalCount = await mealQuery.CountAsync(cancellationToken);
        int totalPage = (totalCount + pageSize - 1) / pageSize;

        IReadOnlyCollection<MealItemDto> mealItems = await mealQuery
            .OrderByDescending(e => e.InTakeTime)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new MealItemDto(
                e.Id,
                e.MealName.Value,
                e.MealDetails.Value,
                (int)e.MealType,
                e.InTakeTime,
                e.NutrientProfile.Calorie.Value,
                e.NutrientProfile.Carbohydrate.Value,
                e.NutrientProfile.Protein.Value,
                e.NutrientProfile.Fat.Value,
                e.NutrientProfile.Calcium.Value,
                e.NutrientProfile.Iron.Value,
                e.NutrientProfile.Magnesium.Value,
                e.NutrientProfile.Zinc.Value,
                e.NutrientProfile.VitaminA.Value,
                e.NutrientProfile.VitaminB.Value,
                e.NutrientProfile.VitaminC.Value,
                e.NutrientProfile.VitaminD.Value,
                e.NutrientProfile.VitaminE.Value
            ))
            .ToListAsync(cancellationToken);

        PagedMealsQueryReponse reponse = new PagedMealsQueryReponse(
            mealItems,
            totalPage,
            totalCount
        );
        return Result<PagedMealsQueryReponse>.Success(reponse);
    }
}