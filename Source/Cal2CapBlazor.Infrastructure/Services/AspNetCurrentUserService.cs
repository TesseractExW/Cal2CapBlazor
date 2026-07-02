using System.Security.Claims;
using Cal2CapBlazor.Application.Common.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace Cal2CapBlazor.Infrastructure.Services;

public class AspNetCurrentUserService(AuthenticationStateProvider authStateProvider)
    : ICurrentUserService
{
    private ClaimsPrincipal? User => authStateProvider
        .GetAuthenticationStateAsync()
        .GetAwaiter()
        .GetResult()
        .User;

    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

    public Guid AccountId
    {
        get
        {
            string? idClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(idClaim, out var guid) ? guid : Guid.Empty;
        }
    }

    public bool HasRole(string role) => User?.IsInRole(role) ?? false;
}
