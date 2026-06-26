using MediatR;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Accounts;

namespace Cal2CapBlazor.Application.Accounts; 

internal sealed class ChangeDisplayNameCommandHandler(IAccountRepository repository)
    : IRequestHandler<ChangeDisplayNameCommand, Result>
{
    public async Task<Result> Handle(ChangeDisplayNameCommand request, CancellationToken cancellationToken)
    {
        Result<AccountEntity> accountResult = await repository.GetByIdAsync(request.AccountId, cancellationToken);
        if (!accountResult.IsSuccess)
        {
            return accountResult;
        }

        AccountEntity account = accountResult.Value;

        Result updateResult = account.SetDisplayName(request.NewDisplayName);
        if (!updateResult.IsSuccess)
        {
            return updateResult; 
        }

        return await repository.UpdateAccountAsync(account, cancellationToken);
    }
}