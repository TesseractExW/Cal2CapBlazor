using Cal2CapBlazor.Domain.Accounts.Exceptions;

namespace Cal2CapBlazor.Domain.Accounts;

public class AccountEntity
{
    #region Description Backing Fields

    private string _email       = string.Empty;
    private string _password    = string.Empty;
    private string _displayName = string.Empty;
    
    #endregion

    #region Descriptions

    public Guid   Id            { get; private set; }
    public string Email         { get => _email;        set => SetEmail(value);         }
    public string Password      { get => _password;     set => SetPassword(value);      }
    public string DisplayName   { get => _displayName;  set => SetDisplayName(value);   }

    #endregion

    public AccountEntity(string email, string password, string displayName)
    {
        Id = Guid.CreateVersion7();
        Email = email;
        Password = password;
        DisplayName = displayName;
    }

    #region Setters

    private void SetEmail(string email)
    {
        EmailInvalidException.ThrowIfInvalid(email);
        _email = email;
    }

    private void SetPassword(string password) 
    {
        PasswordInvalidException.ThrowIfInvalid(password);
        _password = password;
    }
    
    private void SetDisplayName(string displayName) 
    {
        DisplayNameInvalidException.ThrowIfInvalid(displayName);
        _displayName = displayName;
    }

    #endregion
}