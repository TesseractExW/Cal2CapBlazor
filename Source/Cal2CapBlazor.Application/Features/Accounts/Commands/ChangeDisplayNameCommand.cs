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
public record ChangeDisplayNameCommand(
    string NewDisplayName, 
    string Password) 
    : IRequest<Response>;

internal sealed class ChangeDisplayNameCommandHandler(
    IAccountRepository accountRepository, 
    IUserContext userContext,
    IPasswordHasherService passwordHasher)
    : IRequestHandler<ChangeDisplayNameCommand, Response>
{
    public async Task<Response> Handle(ChangeDisplayNameCommand request, CancellationToken cancellationToken)
    {
        Result<Account> accountResult = await accountRepository.GetByIdAsync(userContext.Id, cancellationToken);
        if (!accountResult.IsSuccess)
        {
            return Response.Failure(accountResult.Error);
        }

        Account account = accountResult.Value;
        if (!passwordHasher.Verify(request.Password, account.HashedPassword.Value))
        {
            return Response.Failure(new ErrorResult("ChangeDisplayName.IncorrectPassword", "The password is incorrect"));
        }

        DisplayName newDisplayName = DisplayName.Create(request.NewDisplayName).Value;
        Result updateResult = account.UpdateDisplayName(newDisplayName);
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

public class ChangeDisplayNameCommandValidator : AbstractValidator<ChangeDisplayNameCommand>
{
    public ChangeDisplayNameCommandValidator()
    {
        RuleFor(e => e.NewDisplayName).MustBeValueObject(DisplayName.Create);
    }
}