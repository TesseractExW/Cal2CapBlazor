namespace Cal2CapBlazor.Application.Common.Interfaces;
public interface IPasswordHasherService
{
    bool Verify(string password, string hashedPassword);
    string Hash(string password);
}