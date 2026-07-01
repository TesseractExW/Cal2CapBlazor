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

public record ChangeEmailAddressCommand(
    string NewEmailAddress, 
    string Password) 
    : IRequest<Result<string>>;

[RequireRole("User")]
internal sealed class ChangeEmailAddressCommandHandler(
    IAccountRepository accountRepository,
    ICurrentUserService currentUser, 
    IPasswordHasherService passwordHasher,
    ITokenGenerator tokenGenerator)
    : IRequestHandler<ChangeEmailAddressCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ChangeEmailAddressCommand request, CancellationToken cancellationToken)
    {
        Result<Account> accountResult = await accountRepository.GetByIdAsync(currentUser.AccountId, cancellationToken);
        if (!accountResult.IsSuccess)
        {
            return Result<string>.Failure(accountResult.Error);
        }
        
        Account account = accountResult.Value;
        if (!passwordHasher.Verify(request.Password, account.HashedPassword.Value))
        {
            return Result<string>.Failure(new ErrorResult("ChangeEmail.IncorrectPassword", "The password is incorrect"));
        }

        EmailAddress newEmailAddress = EmailAddress.Create(request.NewEmailAddress).Value;
        Result updateResult = account.UpdateEmailAddress(newEmailAddress);
        if (!updateResult.IsSuccess)
        {
            return Result<string>.Failure(updateResult.Error); 
        }

        Result<Account> getByEmailResult = await accountRepository.GetByEmailAsync(newEmailAddress);
        if (getByEmailResult.isSuccess)
        {
            return Result<string>.Failure(new ErrorResult("ChangeEmail.EmailAlreadyInUse", "The provided email has already been in use."));
        }

        Result repoResult = await accountRepository.UpdateAccountAsync(account, cancellationToken);
        if (!repoResult.IsSuccess)
        {
            return Result<string>.Failure(repoResult.Error);
        }

        string token = tokenGenerator.GenerateToken(account);
        return Result<string>.Success(token);
    }
}

public class ChangeEmailAddressCommandValidator : AbstractValidator<ChangeEmailAddressCommand>
{
    public ChangeEmailAddressCommandValidator()
    {
        RuleFor(e => e.NewEmailAddress).MustBeValueObject(EmailAddress.Create);
    }
}