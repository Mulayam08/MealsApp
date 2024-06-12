namespace MealApp.Views;
using MealApp.Models;
using MealApp.ViewModels;

public partial class MealDetailPage : ContentPage
{
    //TODO : To Define Local Class Level Variables...
    MealDetailPageViewModel MealsPageVM;

    public bool isFirstTimePageLoad = true;

    #region Constructor
    public MealDetailPage(Meal meal)
    {
        InitializeComponent();
        MealsPageVM = new MealDetailPageViewModel(this.Navigation);
        this.BindingContext = MealsPageVM;
        MealsPageVM.SelectedMeal = meal;
    }
    #endregion

    #region Event Handler

    /// <summary>
    /// TODO:To define the page on appearing event...
    /// </summary> 
    protected async override void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await MealsPageVM.GetMealDetails();
        }
        catch (Exception)
        { }
    }

    #endregion
}