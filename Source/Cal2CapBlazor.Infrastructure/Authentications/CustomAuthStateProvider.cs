using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Cal2CapBlazor.Infrastructure.Authentications;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    private ClaimsPrincipal _currentUser;

    public CustomAuthStateProvider()
    {
        _currentUser = _anonymous;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(_currentUser));
    }

    public void NotifyUserLogin(Guid accountId)
    {
        List<Claim> claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, accountId.ToString()),
            new(ClaimTypes.Role, "User") 
        };

        ClaimsIdentity identity = new ClaimsIdentity(claims, "CustomAuthType");
        _currentUser = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
    }

    public void NotifyUserLogout()
    {
        _currentUser = _anonymous;
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
    }
}