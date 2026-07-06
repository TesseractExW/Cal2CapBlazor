using Cal2CapBlazor.Application.Accounts.DataTransferObjects;
using Cal2CapBlazor.Application.Common.Extensions;
using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Domain.Accounts;
using Cal2CapBlazor.Domain.Accounts.ValueObjects;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using FluentValidation;
using MediatR;
using Response = Cal2CapBlazor.Domain.Common.Result<
    Cal2CapBlazor.Application.Accounts.DataTransferObjects.AccountProfileDto>;

namespace Cal2CapBlazor.Application.Accounts.Commands;
public record RegisterAccountCommand(
    string DisplayName, 
    string EmailAddress, 
    string Password, 
    string ConfirmPassword) 
    : IRequest<Response>;

internal sealed class RegisterAccountCommandHandler(
    IAccountRepository accountRepository,
    IPasswordHasherService passwordHasher)
    : IRequestHandler<RegisterAccountCommand, Response>
{
    public async Task<Response> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        EmailAddress emailAddress = EmailAddress.Create(request.EmailAddress).Value;
        Result<Account> getByEmailResult = await accountRepository.GetByEmailAsync(emailAddress);
        if (getByEmailResult.isSuccess)
        {
            return Response.Failure(new ErrorResult("CreateAccount.EmailAlreadyInUse", "The provided email has already been in use."));
        }

        string hashedString = passwordHasher.Hash(request.Password);
        HashedPassword hashedPassword = HashedPassword.Create(hashedString).Value;
        DisplayName displayName = DisplayName.Create(request.DisplayName).Value;

        Account account = new Account(Guid.CreateVersion7(), emailAddress, hashedPassword, displayName);

        Result addResult = await accountRepository.AddAccountAsync(account, cancellationToken);
        if (!addResult.IsSuccess)
        {
            return Response.Failure(addResult.Error);
        }

        return Response.Success(new AccountProfileDto(
            account.Id, 
            account.EmailAddress.Value,
            account.DisplayName.Value));
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
            .WithMessage("The confirmation password cannot be empty or consist only of whitespaces.")
            .Equal(x => x.Password)
            .WithMessage("The passwords do not match.");
    }
}