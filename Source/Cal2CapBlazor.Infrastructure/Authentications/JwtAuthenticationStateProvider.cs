using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components.Authorization;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using Cal2CapBlazor.Application.Common.Security;
using System.IdentityModel.Tokens.Jwt;

namespace Cal2CapBlazor.Infrastructure.Authentications;

public class JwtAuthStateProvider(IConfiguration configuration) : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal anomymous = new ClaimsPrincipal(new ClaimsIdentity());

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? jwtToken = JwtTokenStates.JwtToken;
        if (jwtToken is null)
        {
            return await Task.FromResult(new AuthenticationState(anomymous));
        }

        Result<UserClaims> claimResult = DecryptToken(jwtToken);
        if (!claimResult.isSuccess)
        {
            return await Task.FromResult(new AuthenticationState(anomymous));
        }

        ClaimsPrincipal claimsPrincipal = SetClaimPrinciple(claimResult.Value);
        return await Task.FromResult(new AuthenticationState(claimsPrincipal));
    }

    public async void UpdateAuthenticationState(string jwtToken)
    {
        JwtTokenStates.JwtToken = null;

        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
        Result<UserClaims> claimsResult = DecryptToken(jwtToken);

        if (claimsResult.isSuccess)
        {
            JwtTokenStates.JwtToken = jwtToken;
            claimsPrincipal = SetClaimPrinciple(claimsResult.Value);
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    private Result<UserClaims> DecryptToken(string jwtToken)
    {
        if (string.IsNullOrWhiteSpace(jwtToken))
        {
            return Result<UserClaims>.Failure(new ErrorResult("JwtAuth.Invalid", "Invalid JsonWebToken."));
        }

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        JwtSecurityToken token = handler.ReadJwtToken(jwtToken);

        Claim? email = token.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email);
        Claim? displayName = token.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Name);

        if (email is null || displayName is null)
        {
            return Result<UserClaims>.Failure(new ErrorResult("JwtAuth.Invalid", "Invalid JsonWebToken."));
        }

        return Result<UserClaims>.Success(new UserClaims(email.Value, displayName.Value));
    }

    private ClaimsPrincipal SetClaimPrinciple(UserClaims userClaims)
    {
        if (userClaims.Email is null || userClaims.DisplayName is null)
        {
            return new ClaimsPrincipal();
        }

        return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.Name, userClaims.DisplayName),
            new Claim(ClaimTypes.Email, userClaims.Email)
        }, "JwtAuth"));
    }
}