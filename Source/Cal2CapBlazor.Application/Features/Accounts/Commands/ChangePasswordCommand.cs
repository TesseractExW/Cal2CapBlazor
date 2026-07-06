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
public record ChangePasswordCommand(
    string Password, 
    string NewPassword, 
    string ConfirmPassword) 
    : IRequest<Response>;

internal sealed class ChangePasswordCommandHandler(
    IAccountRepository accountRepository,
    IUserContext userContext,
    IPasswordHasherService passwordHasher)
    : IRequestHandler<ChangePasswordCommand, Response>
{
    public async Task<Response> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        Result<Account> accountResult = await accountRepository.GetByIdAsync(userContext.Id, cancellationToken);
        if (!accountResult.IsSuccess)
        {
            return Response.Failure(accountResult.Error);
        }
        Account account = accountResult.Value;
        if (!passwordHasher.Verify(request.Password, account.HashedPassword.Value))
        {
            return Response.Failure(new ErrorResult("ChangePassword.Incorrect", "The password is incorrect."));
        }

        string hashedString = passwordHasher.Hash(request.NewPassword);
        HashedPassword hashedPassword = HashedPassword.Create(hashedString).Value;

        // throw argument exception
        Result updateResult = account.UpdateHashedPassword(hashedPassword);
        if (!updateResult.IsSuccess)
        {
            return Response.Failure(updateResult.Error);
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

public class ChangePasswordCommandValidation : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidation()
    {
        RuleFor(e => e.NewPassword).MustBeValueObject(Password.Create);

        RuleFor(e => e.ConfirmPassword)
            .NotEmpty()
            .WithMessage("The confirmation password cannot be empty or consist only of whitespaces.")
            .Equal(x => x.Password)
            .WithMessage("The passwords do not match.");
    }
}