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

public record CreateAccountCommand(
    string DisplayName, 
    string EmailAddress, 
    string Password, 
    string ConfirmPassword) 
    : IRequest<Result>;

[GuestOnly]
internal sealed class CreateAccountCommandHandler(
    IAccountRepository accountRepository,
    IPasswordHasherService passwordHasher)
    : IRequestHandler<CreateAccountCommand, Result> 
{
    public async Task<Result> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        EmailAddress emailAddress = EmailAddress.Create(request.EmailAddress).Value;
        Result<Account> getByEmailResult = await accountRepository.GetByEmailAsync(emailAddress);
        if (getByEmailResult.isSuccess)
        {
            return Result.Failure(new ErrorResult("CreateAccount.EmailAlreadyInUse", "The provided email has already been in use."));
        }

        string hashedString = passwordHasher.Hash(request.Password);
        HashedPassword hashedPassword = HashedPassword.Create(hashedString).Value;
        DisplayName displayName = DisplayName.Create(request.DisplayName).Value;

        Account account = new Account(Guid.CreateVersion7(), emailAddress, hashedPassword, displayName);

        return await accountRepository.AddAccountAsync(account);
    }
}

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(e => e.DisplayName).MustBeValueObject(DisplayName.Create);
        RuleFor(e => e.EmailAddress).MustBeValueObject(EmailAddress.Create);
        RuleFor(e => e.Password).MustBeValueObject(Password.Create);

        RuleFor(e => e.ConfirmPassword).MustBeValueObject(Password.Create);
        RuleFor(e => e.ConfirmPassword)
            .Equal(x => x.Password)
            .WithErrorCode("The passwords do not match.");
    }
}