using System.Net.Http.Json;
using System.Security.Claims;
using Cal2CapBlazor.Presentation.Client.DataTransferObjects;
using Microsoft.AspNetCore.Components.Authorization;

namespace Cal2CapBlazor.Presentation.Client.Infrastructure;
public class WasmAuthenticationStateProvider(HttpClient httpClient) : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient = httpClient;
    private AccountProfileView? _currentUser;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_currentUser != null && _currentUser.Id != Guid.Empty)
        {
            return CreateAuthState(_currentUser);
        }
        try
        {
            AccountProfileView? profile = await _httpClient.GetFromJsonAsync<AccountProfileView>("/accounts/queries/get-account-profile");

            if (profile != null && profile.Id != Guid.Empty)
            {
                _currentUser = profile;
                return CreateAuthState(_currentUser);
            }
        }
        catch (HttpRequestException) {}

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public void UpdateState(AccountProfileView? user)
    {
        _currentUser = user;

        AuthenticationState authState;
        if (user != null && user.Id != Guid.Empty && user.Email != string.Empty && user.DisplayName != string.Empty)
        {
            authState = CreateAuthState(user);
        }
        else
        {
            authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    private AuthenticationState CreateAuthState(AccountProfileView user)
    {
        Claim[] claims = 
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.DisplayName)
        };

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "CookieAuth")));
    }
}