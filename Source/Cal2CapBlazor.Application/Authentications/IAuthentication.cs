using Cal2CapBlazor.Application.Authentications.DataTransferObjects;

namespace Cal2CapBlazor.Application.Authentications;
public interface IAuthentication {
    IReadOnlyList<string> Roles    { get; protected set; }

    Guid AccountId          { get; protected set; }
    bool IsAuthenticated    { get; protected set; }

    Task SignInAsync(SignInDTO signInDTO);
    Task SignOutAsync();
}