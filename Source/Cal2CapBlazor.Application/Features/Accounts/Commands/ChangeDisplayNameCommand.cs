using FluentValidation;
using MediatR;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using Cal2CapBlazor.Domain.Accounts;
using Cal2CapBlazor.Domain.Accounts.ValueObjects;
using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Application.Common.Extensions;
using Cal2CapBlazor.Application.Common.Security;

namespace Cal2CapBlazor.Application.Accounts.Commands;

public record ChangeDisplayNameCommand(
    string NewDisplayName, 
    string Password) 
    : IRequest<Result>;

[RequireRole("User")]
internal sealed class ChangeDisplayNameCommandHandler(
    IAccountRepository repository, 
    ICurrentUserService currentUser,
    IPasswordHasher passwordHasher)
    : IRequestHandler<ChangeDisplayNameCommand, Result>
{
    public async Task<Result> Handle(ChangeDisplayNameCommand request, CancellationToken cancellationToken)
    {
        Result<Account> accountResult = await repository.GetByIdAsync(currentUser.AccountId, cancellationToken);
        if (!accountResult.IsSuccess)
        {
            return accountResult;
        }

        Account account = accountResult.Value;
        if (!passwordHasher.Verify(request.Password, account.HashedPassword.Value))
        {
            return Result.Failure(new ErrorResult("ChangeDisplayName.IncorrectPassword", "The password is incorrect"));
        }

        DisplayName newDisplayName = DisplayName.Create(request.NewDisplayName).Value;
        Result updateResult = account.UpdateDisplayName(newDisplayName);
        if (!updateResult.IsSuccess)
        {
            return updateResult; 
        }

        return await repository.UpdateAccountAsync(account, cancellationToken);
    }
}

public class ChangeDisplayNameCommandValidator : AbstractValidator<ChangeDisplayNameCommand>
{
    public ChangeDisplayNameCommandValidator()
    {
        RuleFor(e => e.NewDisplayName).MustBeValueObject(DisplayName.Create);
    }
}