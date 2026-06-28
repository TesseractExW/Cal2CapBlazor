namespace Cal2CapBlazor.Application.Common.Interfaces;

public interface IPasswordHasher
{
    bool Verify(string password, string hashedPassword);
    string Hash(string password);
}