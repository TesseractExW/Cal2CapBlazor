using FluentValidation;
using MediatR;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Accounts;
using Cal2CapBlazor.Domain.Accounts.ValueObjects;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Application.Common.Extensions;
using Cal2CapBlazor.Application.Common.Security;

namespace Cal2CapBlazor.Application.Accounts.Commands;

public record ChangeEmailAddressCommand(string NewEmailAddress, string Password) : IRequest<Result>;

[RequireRole("User")]
internal sealed class ChangeEmailAddressCommandHandler(
    IAccountRepository repository,
    ICurrentUserService currentUser, 
    IPasswordHasher passwordHasher)
    : IRequestHandler<ChangeEmailAddressCommand, Result>
{
    public async Task<Result> Handle(ChangeEmailAddressCommand request, CancellationToken cancellationToken)
    {
        Result<Account> accountResult = await repository.GetByIdAsync(currentUser.AccountId, cancellationToken);
        if (!accountResult.IsSuccess)
        {
            return accountResult;
        }
        
        Account account = accountResult.Value;
        if (!passwordHasher.Verify(request.Password, account.HashedPassword.Value))
        {
            return Result.Failure(new ErrorResult("ChangeEmail.IncorrectPassword", "The password is incorrect"));
        }

        EmailAddress newEmailAddress = EmailAddress.Create(request.NewEmailAddress).Value;
        Result updateResult = account.UpdateEmailAddress(newEmailAddress);
        if (!updateResult.IsSuccess)
        {
            return updateResult; 
        }

        Result<Account> getByEmailResult = await repository.GetByEmailAsync(newEmailAddress);
        if (getByEmailResult.isSuccess)
        {
            return Result.Failure(new ErrorResult("ChangeEmail.EmailAlreadyInUse", "The provided email has already been in use."));
        }

        return await repository.UpdateAccountAsync(account, cancellationToken);
    }
}

public class ChangeEmailAddressCommandValidator : AbstractValidator<ChangeEmailAddressCommand>
{
    public ChangeEmailAddressCommandValidator()
    {
        RuleFor(e => e.NewEmailAddress).MustBeValueObject(EmailAddress.Create);
    }
}