
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
public record LoginAccountCommand(
    string EmailAddress, 
    string Password)
    : IRequest<Response>;

internal sealed class LoginAccountCommandHandler(
    IAccountRepository accountRepository,
    IPasswordHasherService passwordHasher)
    : IRequestHandler<LoginAccountCommand, Response>
{
    public async Task<Response> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
    {
        ErrorResult invalidCredentials = new ErrorResult("LoginCommand.InvalidCredentials", "Invalid email or password.");
        
        EmailAddress emailAddress = EmailAddress.Create(request.EmailAddress).Value;
        Result<Account> getByEmailResult = await accountRepository.GetByEmailAsync(emailAddress);
        if (!getByEmailResult.isSuccess)
        {
            return Response.Failure(invalidCredentials);
        }

        Account account = getByEmailResult.Value;
        if (!passwordHasher.Verify(request.Password, account.HashedPassword.Value))
        {
            return Response.Failure(invalidCredentials);
        }

        return Response.Success(new AccountProfileDto(
            account.Id, 
            account.EmailAddress.Value,
            account.DisplayName.Value));
    }
}

public class LoginAccountCommandValidator : AbstractValidator<LoginAccountCommand>
{
    public LoginAccountCommandValidator()
    {
        RuleFor(e => e.EmailAddress).MustBeValueObject(EmailAddress.Create);
        RuleFor(e => e.Password).MustBeValueObject(Password.Create);
    }
}