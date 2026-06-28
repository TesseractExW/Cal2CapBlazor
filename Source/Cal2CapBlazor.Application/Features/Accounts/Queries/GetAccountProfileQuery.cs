using MediatR;
using Microsoft.EntityFrameworkCore;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Domain.Common.ValueObjects;

namespace Cal2CapBlazor.Application.Accounts.Queries;

public record AccountProfileResponse(string Email, string DisplayName);

public record GetAccountProfitQuery : IRequest<Result<AccountProfileResponse>>;

internal sealed class GetAccountProfitQueryHandler(
    IApplicationDbContext dbContext,
    ICurrentUserService currentUser)
    : IRequestHandler<GetAccountProfitQuery, Result<AccountProfileResponse>>
{
    public async Task<Result<AccountProfileResponse>> Handle(
        GetAccountProfitQuery query, 
        CancellationToken cancellationToken)
    {
       AccountProfileResponse? profile = await dbContext.Accounts
            .AsNoTracking()
            .Where(e => e.Id == currentUser.AccountId)
            .Select(x => new AccountProfileResponse(
                x.EmailAddress.Value,
                x.DisplayName.Value
            ))
            .FirstOrDefaultAsync();

        if (profile is null)
        {
            return Result<AccountProfileResponse>.Failure(new ErrorResult("GetAccountProfile.AccountNotFound", "Account profile could not be found."));
        }

        return Result<AccountProfileResponse>.Success(profile);
    }
}