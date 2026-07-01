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

public record RegisterAccountCommand(
    string DisplayName, 
    string EmailAddress, 
    string Password, 
    string ConfirmPassword) 
    : IRequest<Result<string>>;

[GuestOnly]
internal sealed class RegisterAccountCommandHandler(
    IAccountRepository accountRepository,
    IPasswordHasherService passwordHasher,
    ITokenGenerator tokenGenerator)
    : IRequestHandler<RegisterAccountCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        EmailAddress emailAddress = EmailAddress.Create(request.EmailAddress).Value;
        Result<Account> getByEmailResult = await accountRepository.GetByEmailAsync(emailAddress);
        if (getByEmailResult.isSuccess)
        {
            return Result<string>.Failure(new ErrorResult("CreateAccount.EmailAlreadyInUse", "The provided email has already been in use."));
        }

        string hashedString = passwordHasher.Hash(request.Password);
        HashedPassword hashedPassword = HashedPassword.Create(hashedString).Value;
        DisplayName displayName = DisplayName.Create(request.DisplayName).Value;

        Account account = new Account(Guid.CreateVersion7(), emailAddress, hashedPassword, displayName);

        Result addResult = await accountRepository.AddAccountAsync(account, cancellationToken);
        if (!addResult.IsSuccess)
        {
            return Result<string>.Failure(addResult.Error);
        }

        string token = tokenGenerator.GenerateToken(account);
        return Result<string>.Success(token);
    }
}

public class RegisterAccountCommandValidator : AbstractValidator<RegisterAccountCommand>
{
    public RegisterAccountCommandValidator()
    {
        RuleFor(e => e.DisplayName).MustBeValueObject(DisplayName.Create);
        RuleFor(e => e.EmailAddress).MustBeValueObject(EmailAddress.Create);
        RuleFor(e => e.Password).MustBeValueObject(Password.Create);

        RuleFor(e => e.ConfirmPassword)
            .NotEmpty()
            .WithErrorCode("The confirmation password cannot be empty or consist only of whitespaces.")
            .Equal(x => x.Password)
            .WithErrorCode("The passwords do not match.");
    }
}