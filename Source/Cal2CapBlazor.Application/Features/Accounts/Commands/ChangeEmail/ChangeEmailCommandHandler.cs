using MediatR;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Accounts;

namespace Cal2CapBlazor.Application.Accounts;

internal sealed class ChangeEmailNameCommandHandler(IAccountRepository repository)
    : IRequestHandler<ChangeEmailNameCommand, Result>
{
    public async Task<Result> Handle(ChangeEmailNameCommand request, CancellationToken cancellationToken)
    {
        Result<AccountEntity> accountResult = await repository.GetByIdAsync(request.AccountId, cancellationToken);
        if (!accountResult.IsSuccess)
        {
            return accountResult;
        }
        
        AccountEntity account = accountResult.Value;

        if (account.Email == request.NewEmail)
        {
            return Result.Failure(new ResultError("TODO", "TODO"));
        }

        Result<AccountEntity> emailResult = await repository.GetByEmailAsync(request.NewEmail, cancellationToken);
        if (emailResult.IsSuccess) // Collision
        {
            return Result.Failure(new ResultError("TODO", "TODO"));
        }

        Result updateResult = account.SetEmail(request.NewEmail);
        if (!updateResult.IsSuccess)
        {
            return updateResult;
        }

        return await repository.UpdateAccountAsync(account, cancellationToken);
    }
}