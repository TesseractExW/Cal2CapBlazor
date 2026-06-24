using System.Runtime.CompilerServices;
using Cal2CapBlazor.Domain.Meals.Exceptions;

namespace Cal2CapBlazor.Domain.Meals;
public class MealEntity {
    #region Description Backing Fields

    private string _name = string.Empty;
    private DateTime _inTakeTime;
    
    #endregion

    #region Macronutrient Backing Fields

    private int? _calorie;
    private int? _carbohydrate;
    private int? _protein;
    private int? _fat;

    #endregion

    #region Mineral Backing Fields

    private int? _calcium;
    private int? _iron;
    private int? _magnesium;
    private int? _zinc;

    #endregion

    #region Vitamin Backing Fields

    private int? _vitaminA;
    private int? _vitaminB;
    private int? _vitaminC;
    private int? _vitaminD;
    private int? _vitaminE;

    #endregion

    #region Descriptions

    public Guid     AccountId   { get; private set; }
    public Guid     Id          { get; private set; }
    public string   Name        { get => _name;             set => SetName(value);          }
    public DateTime InTakeTime  { get => _inTakeTime;       set => SetInTakeTime(value);    }

    #endregion

    #region Macronutrients

    public int? Calorie         { get => _calorie;          set => SetQuantity(ref _calorie,        value); }
    public int? Carbohydrate    { get => _carbohydrate;     set => SetQuantity(ref _carbohydrate,   value); }
    public int? Protein         { get => _protein;          set => SetQuantity(ref _protein,        value); }
    public int? Fat             { get => _fat;              set => SetQuantity(ref _fat,            value); }

    #endregion

    #region Minerals

    public int? Calcium         { get => _calcium;          set => SetQuantity(ref _calcium,        value); }
    public int? Iron            { get => _iron;             set => SetQuantity(ref _iron,           value); }
    public int? Magnesium       { get => _magnesium;        set => SetQuantity(ref _magnesium,      value); }
    public int? Zinc            { get => _zinc;             set => SetQuantity(ref _zinc,           value); }

    #endregion

    #region Vitamins
    
    public int? VitaminA        { get => _vitaminA;         set => SetQuantity(ref _vitaminA,       value); }
    public int? VitaminB        { get => _vitaminB;         set => SetQuantity(ref _vitaminB,       value); }
    public int? VitaminC        { get => _vitaminC;         set => SetQuantity(ref _vitaminC,       value); }
    public int? VitaminD        { get => _vitaminD;         set => SetQuantity(ref _vitaminD,       value); }
    public int? VitaminE        { get => _vitaminE;         set => SetQuantity(ref _vitaminE,       value); }

    #endregion

    public MealEntity(Guid accountId)
    {
        AccountId = accountId;
        Id = Guid.CreateVersion7();
    }

    #region Setters

    private void SetName(string value)
    {
        NameInvalidException.ThrowIfInvalid(value);
        _name = value;
    }

    private void SetInTakeTime(DateTime value)
    {
        InTakeTimeInvalidException.ThrowIfInvalid(value);
        _inTakeTime = value;
    }
    
    private void SetQuantity(ref int? dest, int? value, [CallerMemberName] string? propertyName = null)
    {
        NegativeQuantityException.ThrowIfNegative(value, propertyName);
        dest = value;
    }

    #endregion
}