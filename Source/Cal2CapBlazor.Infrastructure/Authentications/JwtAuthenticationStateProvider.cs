using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Cal2CapBlazor.Application.Accounts.DataTransferObjects;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using Microsoft.AspNetCore.Components.Authorization;

namespace Cal2CapBlazor.Infrastructure.Authentications;
public class JwtTokenStates
{
    public string? JwtToken { get; set; } = null; 
}

public class JwtAuthStateProvider() : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal anomymous = new ClaimsPrincipal(new ClaimsIdentity());
    private JwtTokenStates states = new JwtTokenStates();

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? jwtToken = states.JwtToken;
        if (jwtToken is null)
        {
            return await Task.FromResult(new AuthenticationState(anomymous));
        } 

        Result<AccountAuthenticationDto> claimResult = DecryptToken(jwtToken);
        if (!claimResult.isSuccess)
        {
            return await Task.FromResult(new AuthenticationState(anomymous));
        }

        ClaimsPrincipal claimsPrincipal = SetClaimPrinciple(claimResult.Value);
        return await Task.FromResult(new AuthenticationState(claimsPrincipal));
    }

    public async void UpdateAuthenticationState(string jwtToken)
    {
        states.JwtToken = null;

        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
        Result<AccountAuthenticationDto> claimsResult = DecryptToken(jwtToken);

        if (claimsResult.isSuccess)
        {
            states.JwtToken = jwtToken;
            claimsPrincipal = SetClaimPrinciple(claimsResult.Value);
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    private Result<AccountAuthenticationDto> DecryptToken(string jwtToken)
    {
        if (string.IsNullOrWhiteSpace(jwtToken))
        {
            return Result<AccountAuthenticationDto>.Failure(new ErrorResult("JwtAuth.Invalid", "Invalid JsonWebToken."));
        }

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        JwtSecurityToken token = handler.ReadJwtToken(jwtToken);

        Claim? id = token.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier);
        Claim? email = token.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email);

        if (id is null || email is null)
        {
            return Result<AccountAuthenticationDto>.Failure(new ErrorResult("JwtAuth.Invalid", "Invalid JsonWebToken."));
        }
        
        AccountAuthenticationDto authDto = new AccountAuthenticationDto(Guid.Parse(id.Value), email.Value);
        return Result<AccountAuthenticationDto>.Success(authDto);
    }

    private ClaimsPrincipal SetClaimPrinciple(AccountAuthenticationDto authDto)
    {
        if (authDto.Id is null || authDto.Email is null)
        {
            return new ClaimsPrincipal();
        }
        return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, authDto.Id.ToString()!),
            new Claim(JwtRegisteredClaimNames.Sub, authDto.Id.ToString()!),
            new Claim(JwtRegisteredClaimNames.Email, authDto.Email)
        }, "JwtAuth"));
    }
}