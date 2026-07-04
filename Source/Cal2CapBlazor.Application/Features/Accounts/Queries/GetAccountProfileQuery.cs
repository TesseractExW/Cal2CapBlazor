using Cal2CapBlazor.Application.Accounts.DataTransferObjects;
using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cal2CapBlazor.Application.Accounts.Queries;
public record GetAccountProfitQuery : IRequest<Result<AccountProfileDto>>;

internal sealed class GetAccountProfitQueryHandler(
    IApplicationDbContext dbContext,
    IUserContext userContext)
    : IRequestHandler<GetAccountProfitQuery, Result<AccountProfileDto>>
{
    public async Task<Result<AccountProfileDto>> Handle(
        GetAccountProfitQuery query, 
        CancellationToken cancellationToken)
    {
       AccountProfileDto? profile = await dbContext.Accounts
            .AsNoTracking()
            .Where(e => e.Id == userContext.Id)
            .Select(x => new AccountProfileDto(
                x.Id,
                x.EmailAddress.Value,
                x.DisplayName.Value
            ))
            .FirstOrDefaultAsync();

        if (profile is null)
        {
            return Result<AccountProfileDto>.Failure(new ErrorResult("GetAccountProfile.AccountNotFound", "Account profile could not be found."));
        }
        return Result<AccountProfileDto>.Success(profile);
    }
}