using Cal2CapBlazor.Domain.Accounts;

namespace Cal2CapBlazor.Application.Common.Interfaces;

public interface ITokenGenerator
{
    string GenerateToken(Account account);
}