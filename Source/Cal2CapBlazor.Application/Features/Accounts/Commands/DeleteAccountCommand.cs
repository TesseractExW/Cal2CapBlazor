using Cal2CapBlazor.Application.Common.Extensions;
using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Domain.Accounts;
using Cal2CapBlazor.Domain.Accounts.ValueObjects;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using FluentValidation;
using MediatR;

namespace Cal2CapBlazor.Application.Accounts.Commands;
public record DeleteAccountCommand(
    string EmailAddress, 
    string Password, 
    string ConfirmPassword) 
    : IRequest<Result>;

internal sealed class DeleteAccountCommandHandler(
    IAccountRepository accountRepository,
    IUserContext userContext,
    IPasswordHasherService passwordHasher)
    : IRequestHandler<DeleteAccountCommand, Result> 
{
    public async Task<Result> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        Result<Account> accountResult = await accountRepository.GetByIdAsync(userContext.Id, cancellationToken);
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

        return Result.Success();
    }
}

public class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
{
    public DeleteAccountCommandValidator()
    {
        RuleFor(e => e.EmailAddress).MustBeValueObject(EmailAddress.Create);
        
        RuleFor(e => e.ConfirmPassword)
            .NotEmpty()
            .WithMessage("The confirmation password cannot be empty or consist only of whitespaces.")
            .Equal(x => x.Password)
            .WithMessage("The passwords do not match.");
    }
}