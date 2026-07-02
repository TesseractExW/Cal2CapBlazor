namespace Cal2CapBlazor.Application.Common.Interfaces;
public interface ICurrentUserService
{
    bool IsAuthenticated { get; }
    Guid AccountId { get; }
    bool HasRole(string Role);
}