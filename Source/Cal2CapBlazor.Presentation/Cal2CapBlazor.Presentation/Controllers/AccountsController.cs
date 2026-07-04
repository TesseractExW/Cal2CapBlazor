using System.Security.Claims;
using Cal2CapBlazor.Application.Accounts.Commands;
using Cal2CapBlazor.Application.Accounts.DataTransferObjects;
using Cal2CapBlazor.Application.Accounts.Queries;
using Cal2CapBlazor.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using Response = Cal2CapBlazor.Domain.Common.Result<
    Cal2CapBlazor.Application.Accounts.DataTransferObjects.AccountProfileDto>;

namespace Cal2CapBlazor.Presentation.Controllers;
[ApiController]
[Route("accounts")]
public class AccountsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    // --- COMMANDS ---

    [AllowAnonymous]
    [HttpPost("commands/register-account")]
    public async Task<IActionResult> RegisterAccount([FromBody] RegisterAccountCommand command)
    {
        Response result = await _mediator.Send(command);
        return await HandleAuthCommandResult(result);
    }

    [AllowAnonymous]
    [HttpPost("commands/login")]
    public async Task<IActionResult> LoginAccount([FromBody] LoginAccountCommand command)
    {
        Response result = await _mediator.Send(command);
        return await HandleAuthCommandResult(result);
    }

    [Authorize]
    [HttpPost("commands/logout")]
    public async Task<IActionResult> LoginAccount()
    {
        await HttpContext.SignOutAsync();
        return NoContent();
    }

    [Authorize]
    [HttpPut("commands/change-display-name")]
    public async Task<IActionResult> ChangeDisplayName([FromBody] ChangeDisplayNameCommand command)
    {
        Response result = await _mediator.Send(command);
        return await HandleAuthCommandResult(result);
    }

    [Authorize]
    [HttpPut("commands/change-email-address")]
    public async Task<IActionResult> ChangeEmailAddress([FromBody] ChangeEmailAddressCommand command)
    {
        Response result = await _mediator.Send(command);
        return await HandleAuthCommandResult(result);
    }

    [Authorize]
    [HttpPut("commands/change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        Response result = await _mediator.Send(command);
        return await HandleAuthCommandResult(result);
    }

    [Authorize]
    [HttpDelete("commands/delete-account")]
    public async Task<IActionResult> DeleteAccount([FromBody] DeleteAccountCommand command)
    {
        Result result = await _mediator.Send(command);
        if (!result.IsSuccess) return BadRequest(result.Error);

        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return NoContent();
    }

    // --- QUERIES ---

    [Authorize]
    [HttpGet("queries/get-account-profile")]
    public async Task<IActionResult> GetAccountProfile()
    {
        Response result = await _mediator.Send(new GetAccountProfitQuery());
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    // --- HELPER METHODS ---

    private async Task<IActionResult> HandleAuthCommandResult(Response result)
    {
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        AccountProfileDto dto = result.Value!;
        
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()),
            new Claim(ClaimTypes.Email, dto.Email),
            new Claim(ClaimTypes.Name, dto.DisplayName)
        };

        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(identity));

        return Ok(new AccountProfileDto(dto.Id, dto.Email, dto.DisplayName));
    }
}