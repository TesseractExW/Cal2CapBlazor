using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cal2CapBlazor.Domain.Accounts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cal2CapBlazor.Infrastructure.Services.Auths;
public class JwtTokenGenerator(IConfiguration configuration)
{
    public string GenerateToken(Account account)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        Claim[] userClaims =
        {
            new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new Claim(ClaimTypes.Name, account.DisplayName.Value),
            new Claim(ClaimTypes.Email, account.EmailAddress.Value)
        };

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddDays(3),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}