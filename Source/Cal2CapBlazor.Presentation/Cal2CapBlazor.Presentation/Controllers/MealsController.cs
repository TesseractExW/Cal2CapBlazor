using Cal2CapBlazor.Application.Meals.Commands;
using Cal2CapBlazor.Application.Meals.Queries;
using Cal2CapBlazor.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cal2CapBlazor.Presentation.Controllers;
[ApiController]
[Route("meals")]
[Authorize]
public class MealsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    // --- COMMANDS ---

    [HttpPost("commands/create-meal")]
    public async Task<IActionResult> CreateMeal([FromBody] CreateMealCommand command)
    {
        Result result = await _mediator.Send(command);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    [HttpPut("commands/change-in-take-time")]
    public async Task<IActionResult> ChangeMealIntakeTime([FromBody] ChangeMealInTakeTimeCommand command)
    {
        Result result = await _mediator.Send(command);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    [HttpPut("commands/change-meal-details")]
    public async Task<IActionResult> ChangeMealDetails([FromBody] ChangeMealDetailsCommand command)
    {
        Result result = await _mediator.Send(command);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    [HttpPut("commands/change-meal-name")]
    public async Task<IActionResult> ChangeMealName([FromBody] ChangeMealNameCommand command)
    {
        Result result = await _mediator.Send(command);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    [HttpPut("commands/change-meal-nutrient")]
    public async Task<IActionResult> ChangeMealNutrient([FromBody] ChangeMealNutrientCommand command)
    {
        Result result = await _mediator.Send(command);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    [HttpPut("commands/change-meal-type")]
    public async Task<IActionResult> ChangeMealType([FromBody] ChangeMealTypeCommand command)
    {
        Result result = await _mediator.Send(command);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    [HttpDelete("commands/delete-meal")]
    public async Task<IActionResult> DeleteMeal([FromBody] DeleteMealCommand command)
    {
        Result result = await _mediator.Send(command);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    // --- QUERIES ---

    [HttpGet("queries/get-paged-meals")]
    public async Task<IActionResult> GetPagedMeals([FromQuery] GetPagedMealsQuery query)
    {
        // [FromQuery] automatically binds properties from the URL 
        // e.g., ?PageIndex=1&PageSize=10&SearchTerm=apple
        Result<PagedMealsQueryReponse> result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}