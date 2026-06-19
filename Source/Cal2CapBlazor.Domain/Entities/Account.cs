using System.ComponentModel.DataAnnotations;

namespace Cal2CapBlazor.Domain.Entites;
/// <summary> Represents a user account and their configuration settings. </summary>
public class Account {
    #region Account description
    [Key]
    public int Id { get; set; }
    [StringLength(50)]
    [EmailAddress]
    /// <summary> The user's registered email address. </summary>
    public required string Email { get; set; }
    [StringLength(50)]
    /// <summary> The user's hashed or encrypted password. </summary>
    public required string Password { get; set; }
    [StringLength(20)]
    /// <summary> The public display name of the user. </summary>
    public required string DisplayName { get; set; }
    #endregion

    #region Meals
    /// <summary> The collection of logged meal details associated with this account. </summary>
    public required List<Meal> Meals { get; set; } = new List<Meal>();
    #endregion
}