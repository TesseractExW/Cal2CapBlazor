using Cal2CapBlazor.Domain.Accounts.Constants;
using Cal2CapBlazor.Domain.Common.Results;

namespace Cal2CapBlazor.Domain.Accounts;

public class AccountEntity
{
    #region Description Backing Fields

    private string _email               = string.Empty;
    private string _hashedPassword      = string.Empty;
    private string _displayName         = string.Empty;
    
    #endregion

    #region Descriptions

    public Guid   Id                    { get; }
    public string Email                 { get => _email;            }
    public string HashedPassword        { get => _hashedPassword;   }
    public string DisplayName           { get => _displayName;      }

    #endregion

    protected AccountEntity()
    {
        Id = Guid.CreateVersion7();
    }

    public static Result<AccountEntity> Create(string email, string hashedPassword, string displayName)
    {
        Result result;
        AccountEntity account = new AccountEntity();

        result = account.SetEmail(email);
        if (!result.IsSuccess)
        {
            return Result<AccountEntity>.Failure(result.Error);
        }

        result = account.SetHashedPassword(hashedPassword);
        if (!result.IsSuccess)
        {
            return Result<AccountEntity>.Failure(result.Error);
        }

        result = account.SetDisplayName(displayName);
        if (!result.IsSuccess)
        {
            return Result<AccountEntity>.Failure(result.Error);
        }

        return Result<AccountEntity>.Success(account);
    }
    #region Setters

    public Result SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result.Failure(AccountErrors.EmailNullOrWhiteSpace);
        }
        else if (!AccountConstant.EmailRegex.IsMatch(email))
        {
            return Result.Failure(AccountErrors.EmailInvalidFormat);
        }

        _email = email;
        return Result.Success();
    }

    public Result SetHashedPassword(string hashedPassword) 
    {
        if (hashedPassword.Length < AccountConstant.MinimumHashedPasswordLength || 
            hashedPassword.Length > AccountConstant.MaximumHashedPasswordLength)
        {
            return Result.Failure(AccountErrors.HashedPasswordLengthOutOfRange);
        }

        _hashedPassword = hashedPassword;
        return Result.Success();
    }
    
    public Result SetDisplayName(string displayName) 
    {
        if (displayName.Length < AccountConstant.MinimumDisplayNameLength || 
            displayName.Length > AccountConstant.MaximumDisplayNameLength)
        {
            return Result.Failure(AccountErrors.DisplayNameLengthOutOfRange);
        }

        _displayName = displayName;
        return Result.Success();
    }

    #endregion
}