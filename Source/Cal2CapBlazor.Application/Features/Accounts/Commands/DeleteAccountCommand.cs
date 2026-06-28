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

public record DeleteAccountCommand(
    string EmailAddress, 
    string Password, 
    string ConfirmPassword) 
    : IRequest<Result>;

[RequireRole("User")]
internal sealed class DeleteAccountCommandHandler(
    IAccountRepository repository,
    ICurrentUserService currentUser,
    IPasswordHasher passwordHasher)
    : IRequestHandler<CreateAccountCommand, Result> 
{
    public async Task<Result> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        Result<Account> accountResult = await repository.GetByIdAsync(currentUser.AccountId, cancellationToken);
        if (!accountResult.IsSuccess)
        {
            return accountResult;
        }

        Account account = accountResult.Value;
        if (!passwordHasher.Verify(request.Password, account.HashedPassword.Value) ||
            request.EmailAddress != account.DisplayName.Value)
        {
            return Result.Failure(new ErrorResult("DeleteAccount.Incorrect", "the password or the email is incorrect"));
        }

        return await repository.DeleteAccountAsync(account);
    }
}

public class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
{
    public DeleteAccountCommandValidator()
    {
        RuleFor(e => e.EmailAddress).MustBeValueObject(EmailAddress.Create);
        
        RuleFor(e => e.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.Password)
            .WithErrorCode("The passwords do not match.");
    }
}