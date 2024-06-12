namespace MealApp.Views; 
using MealApp.Models;
using MealApp.ViewModels;

public partial class CategoryPage : ContentPage
{
    //TODO : To Define Local Class Level Variables...
    CategoryPageViewModel CategoryPageVM;

    public bool isFirstTimePageLoad = true;
     
    #region Constructor
    public CategoryPage()
    {
        InitializeComponent();
        CategoryPageVM = new CategoryPageViewModel(this.Navigation);
        this.BindingContext = CategoryPageVM; 
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
            await CategoryPageVM.GetAllCategories();
        }
        catch (Exception)
        { }
    }

    #endregion
}