using Cal2CapBlazor.Application.Accounts.DataTransferObjects;

namespace Cal2CapBlazor.Application.Common.Interfaces;
public interface IUserContext
{
    Guid Id { get; }
    bool IsAuthenticated { get; }
}