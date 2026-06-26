using Cal2CapBlazor.Domain.Common;

namespace Cal2CapBlazor.Application.Accounts;

public static class AccountRepositoryErrors
{
    public static readonly ResultError AccountNotFound =
        new ResultError(
            "AccountRepositoryErrors.AccountNotFound",
            "The account does not exit in database."
        );
}