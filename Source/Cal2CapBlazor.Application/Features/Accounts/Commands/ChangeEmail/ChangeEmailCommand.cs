using MediatR;
using Cal2CapBlazor.Domain.Common;

namespace Cal2CapBlazor.Application.Accounts;

public record ChangeEmailNameCommand(Guid AccountId, string NewEmail, string Password) : IRequest<Result> {}