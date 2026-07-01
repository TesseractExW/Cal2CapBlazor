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
    : IRequest<Result<string>>;

[RequireRole("User")]
internal sealed class ChangePasswordCommandHandler(
    IAccountRepository accountRepository,
    ICurrentUserService currentUser,
    IPasswordHasherService passwordHasher,
    ITokenGenerator tokenGenerator)
    : IRequestHandler<ChangePasswordCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        Result<Account> accountResult = await accountRepository.GetByIdAsync(currentUser.AccountId, cancellationToken);
        if (!accountResult.IsSuccess)
        {
            return Result<string>.Failure(accountResult.Error);
        }

        Account account = accountResult.Value;
        if (!passwordHasher.Verify(request.Password, account.HashedPassword.Value))
        {
            return Result<string>.Failure(new ErrorResult("ChangePassword.Incorrect", "The password is incorrect."));
        }

        string hashedString = passwordHasher.Hash(request.NewPassword);
        HashedPassword hashedPassword = HashedPassword.Create(hashedString).Value;
        // throw argument exception

        Result updateResult = account.UpdateHashedPassword(hashedPassword);
        if (!updateResult.IsSuccess)
        {
            return Result<string>.Failure(updateResult.Error);
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