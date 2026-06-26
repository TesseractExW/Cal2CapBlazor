using MediatR;
using Cal2CapBlazor.Domain.Common;

namespace Cal2CapBlazor.Application.Accounts;

public record ChangeDisplayNameCommand(Guid AccountId, string NewDisplayName) : IRequest<Result> {}