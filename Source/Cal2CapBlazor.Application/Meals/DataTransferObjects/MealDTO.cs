namespace Cal2CapBlazor.Application.Meals.DataTransferObjects;
public class MealDTO {
    public string   Name        { get; set; } = string.Empty;
    public DateTime InTakeTime  { get; set; }

    #region Macronutrients
    public int? Calorie         { get; set; }
    public int? Carbohydrate    { get; set; }   
    public int? Protein         { get; set; }
    public int? Fat             { get; set; }
    #endregion

    #region Minerals
    public int? Calcium         { get; set; }
    public int? Iron            { get; set; }
    public int? Magnesium       { get; set; }
    public int? Zinc            { get; set; }
    #endregion

    #region Vitamins
    public int? VitaminA        { get; set; }
    public int? VitaminB        { get; set; }
    public int? VitaminC        { get; set; }
    public int? VitaminD        { get; set; }
    public int? VitaminE        { get; set; }
    #endregion
};