using System.Text.RegularExpressions;

namespace Cal2CapBlazor.Domain.Accounts;

public static class AccountConstants
{
    #region Length Related

    public static readonly int MinimumHashedPasswordLength  = 64;
    public static readonly int MaximumHashedPasswordLength  = 72;
    public static readonly int MinimumDisplayNameLength     = 3;
    public static readonly int MaximumDisplayNameLength     = 24;

    #endregion

    public static readonly Regex EmailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$", 
        RegexOptions.Compiled | RegexOptions.IgnoreCase, 
        TimeSpan.FromMilliseconds(250)
    );
}