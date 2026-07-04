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
public record ChangeEmailAddressCommand(
    string NewEmailAddress, 
    string Password) 
    : IRequest<Response>;

internal sealed class ChangeEmailAddressCommandHandler(
    IAccountRepository accountRepository,
    IUserContext userContext, 
    IPasswordHasherService passwordHasher)
    : IRequestHandler<ChangeEmailAddressCommand, Response>
{
    public async Task<Response> Handle(ChangeEmailAddressCommand request, CancellationToken cancellationToken)
    {

        Result<Account> accountResult = await accountRepository.GetByIdAsync(userContext.Id, cancellationToken);
        if (!accountResult.IsSuccess)
        {
            return Response.Failure(accountResult.Error);
        }
        
        Account account = accountResult.Value;
        if (!passwordHasher.Verify(request.Password, account.HashedPassword.Value))
        {
            return Response.Failure(new ErrorResult("ChangeEmail.IncorrectPassword", "The password is incorrect"));
        }

        EmailAddress newEmailAddress = EmailAddress.Create(request.NewEmailAddress).Value;
        Result updateResult = account.UpdateEmailAddress(newEmailAddress);
        if (!updateResult.IsSuccess)
        {
            return Response.Failure(updateResult.Error); 
        }

        Result<Account> getByEmailResult = await accountRepository.GetByEmailAsync(newEmailAddress);
        if (getByEmailResult.isSuccess)
        {
            return Response.Failure(new ErrorResult("ChangeEmail.EmailAlreadyInUse", "The provided email has already been in use."));
        }

        Result repoResult = await accountRepository.UpdateAccountAsync(account, cancellationToken);
        if (!repoResult.IsSuccess)
        {
            return Response.Failure(repoResult.Error);
        }

        return Response.Success(new AccountProfileDto(
            account.Id, 
            account.EmailAddress.Value,
            account.DisplayName.Value));
    }
}

public class ChangeEmailAddressCommandValidator : AbstractValidator<ChangeEmailAddressCommand>
{
    public ChangeEmailAddressCommandValidator()
    {
        RuleFor(e => e.NewEmailAddress).MustBeValueObject(EmailAddress.Create);
    }
}