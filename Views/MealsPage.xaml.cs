namespace MealApp.Views;

using Android.Webkit;
using MealApp.Models;
using MealApp.ViewModels;

public partial class MealsPage : ContentPage
{
    //TODO : To Define Local Class Level Variables...
    MealsPageViewModel MealsPageVM;

    public bool isFirstTimePageLoad = true;
     
    #region Constructor
    public MealsPage(Category category)
	{
		InitializeComponent();
        MealsPageVM = new MealsPageViewModel(this.Navigation);
        this.BindingContext = MealsPageVM;
        MealsPageVM.SelectedCategory = category;
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
            await MealsPageVM.GetAllMeals();
        }
        catch (Exception)
        { }
    }
     
    #endregion 
}