using Microsoft.AspNetCore.Identity;

namespace Cal2CapBlazor.Application.Common.Interfaces;

public class AspNetPasswordHasher : IPasswordHasherService
{
    private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

    public bool Verify(string password, string hashedPassword)
    {
        return _passwordHasher.VerifyHashedPassword("dummyUser", hashedPassword, password) 
            is PasswordVerificationResult.Success;
    }
    public string Hash(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        return _passwordHasher.HashPassword("dummyUser", password);
    }
}