using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using System.Collections.ObjectModel; 
using MealApp.Models; 

namespace MealApp.ViewModels
{
    public class CategoryPageViewModel : BaseViewModel
    {
        #region Variables  
        #endregion

        #region Constructor
        public CategoryPageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                CategoryTapCommand = new Command<Category>(onCategoryTapCommandFunc);
            }
            catch (Exception ex)
            { }
        }
         
        #endregion

        #region Command  
        public Command CategoryTapCommand { get; set; }

        #endregion

        #region Properties
         
        private ObservableCollection<Category> _AllCategoryNodeList = new ObservableCollection<Category>();
        public ObservableCollection<Category> AllCategoryList
        {
            get { return _AllCategoryNodeList; }
            set
            {
                if (_AllCategoryNodeList != value)
                {
                    _AllCategoryNodeList = value;
                    OnPropertyChanged("AllCategoryList");
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// TODO:To get cart count...
        /// </summary>
        /// <returns></returns>
        public async Task GetAllCategories()
        {
            //Call api..
            try
            {
                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                {
                    if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                    {
                        UserDialogs.Instance.ShowLoading();
                        await Task.Run(async () =>
                        {
                            if (_businessCode != null)
                            {
                                await _businessCode.GetAllCategrory(
                                async (obj) =>
                                {
                                    Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                    {
                                        var requestList = obj as CategoryModel;
                                        if (requestList != null)
                                        {
                                            if (requestList.categories != null)
                                            {
                                                AllCategoryList = new ObservableCollection<Category>(requestList.categories);
                                            }
                                        }
                                        UserDialog.HideLoading();
                                    });
                                }, (objj) =>
                                {
                                    Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                    {
                                        UserDialog.HideLoading();
                                        UserDialogs.Instance.Alert("Something went wrong!  Please try again.");
                                    });
                                });
                            }
                        }).ConfigureAwait(false);
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync("No Network Connection found, Please try again!", "", "Okay");
                    }
                });
            }
            catch (Exception ex)
            { UserDialog.HideLoading(); await Task.CompletedTask; }

            await Task.CompletedTask;

        }

        /// <summary>
        /// TODO : To define Tap on Category  Command....
        /// </summary>
        /// <param name="category"></param>
        private void onCategoryTapCommandFunc(Category category)
        { 
            if (category != null)
            {
                Navigation.PushAsync(new Views.MealsPage(category), false);
            } 
        }
        #endregion
    }
}
