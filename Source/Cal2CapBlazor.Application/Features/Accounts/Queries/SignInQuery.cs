using MediatR;
using Microsoft.EntityFrameworkCore;
using Cal2CapBlazor.Domain.Common;
using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Domain.Common.ValueObjects;
using Cal2CapBlazor.Application.Common.Security;
using Cal2CapBlazor.Domain.Accounts;
using Cal2CapBlazor.Domain.Accounts.ValueObjects;

namespace Cal2CapBlazor.Application.Accounts.Queries;

public record SignInQuery(string Email, string Password) : IRequest<Result>;

[GuestOnly]
internal sealed class SignInQueryHandler(
    IApplicationDbContext dbContext,
    IPasswordHasherService passwordHasher)
    : IRequestHandler<SignInQuery, Result>
{
    public async Task<Result> Handle(
        SignInQuery query, 
        CancellationToken cancellationToken)
    {
        EmailAddress emailAddress = EmailAddress.Create(query.Email).Value;
        Account? account = await dbContext.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EmailAddress == emailAddress, cancellationToken);

        if (account is null || !passwordHasher.Verify(query.Password, account.HashedPassword.Value))
        {
            return Result.Failure(new ErrorResult("Auth.Incorrect", "The email address or password provided is incorrect."));
        }

        return Result.Success();
    }
}