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

public record ChangePasswordCommand(
    string Password, 
    string NewPassword, 
    string ConfirmPassword) 
    : IRequest<Result>;

[RequireRole("User")]
internal sealed class ChangePasswordCommandHandler(
    IAccountRepository accountRepository,
    ICurrentUserService currentUser,
    IPasswordHasher passwordHasher)
    : IRequestHandler<ChangePasswordCommand, Result>
{
    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        Result<Account> accountResult = await accountRepository.GetByIdAsync(currentUser.AccountId, cancellationToken);
        if (!accountResult.IsSuccess)
        {
            return accountResult;
        }

        Account account = accountResult.Value;
        if (!passwordHasher.Verify(request.Password, account.HashedPassword.Value))
        {
            return Result.Failure(new ErrorResult("ChangePassword.Incorrect", "The password is incorrect."));
        }

        string hashedString = passwordHasher.Hash(request.NewPassword);
        HashedPassword hashedPassword = HashedPassword.Create(hashedString).Value;
        // throw argument exception

        Result updateResult = account.UpdateHashedPassword(hashedPassword);
        if (!updateResult.IsSuccess)
        {
            return updateResult;
        }

        return await accountRepository.UpdateAccountAsync(account, cancellationToken);
    }
}

public class ChangePasswordCommandValidation : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidation()
    {
        RuleFor(e => e.NewPassword).MustBeValueObject(Password.Create);

        RuleFor(e => e.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.NewPassword)
            .WithErrorCode("The passwords do not match.");
    }
}