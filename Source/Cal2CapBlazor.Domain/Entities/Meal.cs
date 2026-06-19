using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cal2CapBlazor.Domain.Entites;
/// <summary> Represents the nutritional breakdown of a meal. </summary>
public class Meal {
    #region Meal description
    [Key]
    public int Id { get; set; } 
    /// <summary> The foreign key identifier for the associated <see cref="Account"/>. </summary>
    public required int AccountId { get; set; }
    /// <summary> The navigation property for the related <see cref="Account"/>. </summary>
    [ForeignKey(nameof(AccountId))]
    public required Account Account { get; set; }
    /// <summary> The meal name.</summary>
    [StringLength(50)]
    public required string Name { get; set; } 
    /// <summary> The time when the meal is taken. </summary>
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public required DateTime InTakeTime { get; set; }
    #endregion

    #region Macronutrition
    /// <summary> Measured in kilocalories (kcal). </summary>
    public int? Calorie { get; set; }
    /// <summary> Measured in grams (g). </summary>
    public int?Carbohydrate { get; set; }
    /// <summary> Measured in grams (g). </summary>
    public int? Protein { get; set; }
    /// <summary> Measured in grams (g). </summary>
    public int? Fat { get; set; }
    #endregion

    #region Minerals
    /// <summary> Measured in milligrams (mg). </summary>
    public int? Calcium { get; set; }
    /// <summary> Measured in milligrams (mg). </summary>
    public int? Iron { get; set; }
    /// <summary> Measured in milligrams (mg). </summary>
    public int? Magnesium { get; set; }
    /// <summary> Measured in milligrams (mg). </summary>
    public int? Potassium { get; set; }
    /// <summary> Measured in milligrams (mg). </summary>
    public int? Zinc { get; set; }
    #endregion

    #region Vitamins
    /// <summary> Measured in micrograms (mcg). </summary>
    public int? VitaminA { get; set; }
    /// <summary> Measured in milligrams (mg). </summary>
    public int? VitaminB { get; set; }
    /// <summary> Measured in milligrams (mg). </summary>
    public int? VitaminC { get; set; }
    /// <summary> Measured in micrograms (mcg). </summary>
    public int? VitaminD { get; set; }
    /// <summary> Measured in milligrams (mg). </summary>
    public int? VitaminE { get; set; }
    #endregion
}