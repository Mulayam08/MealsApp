using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;
using Acr.UserDialogs;
using System.Collections.ObjectModel;
using MealApp.Models;

namespace MealApp.ViewModels
{
    public class MealsPageViewModel : BaseViewModel
    {
        #region Variables  
        #endregion

        #region Constructor
        public MealsPageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(OnBackCommandAsync);
                MealTapCommand = new Command<Meal>(onMealCommandFunc);
            }
            catch (Exception ex)
            { }
        }

        #endregion

        #region Command  
        public Command MealTapCommand { get; set; }
        public Command BackCommand { get; set; } 

        #endregion

        #region Properties

        private ObservableCollection<Meal> _AllMealList = new ObservableCollection<Meal>();
        public ObservableCollection<Meal> AllMealList
        {
            get { return _AllMealList; }
            set
            {
                if (_AllMealList != value)
                {
                    _AllMealList = value;
                    OnPropertyChanged("AllMealList");
                }
            }
        }

        private Category _SelectedCategory;
        public Category SelectedCategory
        {
            get { return _SelectedCategory; }
            set
            {
                if (_SelectedCategory != value)
                {
                    _SelectedCategory = value;
                    OnPropertyChanged("SelectedCategory");
                }
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// To Do: Back Command
        /// </summary>
        /// <param name="obj"></param>
        private void OnBackCommandAsync(object obj)
        {
            try
            {
                Navigation.PopAsync(false);
            }
            catch (Exception ex)
            { }
        }
        /// <summary>
        /// TODO:To get cart count...
        /// </summary>
        /// <returns></returns>
        public async Task GetAllMeals()
        {
            //Call api..
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    UserDialogs.Instance.ShowLoading();
                    await Task.Run(async () =>
                    {
                        if (_businessCode != null)
                        {
                            await _businessCode.GetAllMeals(SelectedCategory.strCategory,
                            async (obj) =>
                            {
                                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                {
                                    var requestList = obj as MealsModel;
                                    if (requestList != null)
                                    {
                                        if (requestList.meals != null)
                                        {
                                            AllMealList = new ObservableCollection<Meal>(requestList.meals);
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
            }
            catch (Exception ex)
            { UserDialog.HideLoading(); await Task.CompletedTask; }

            await Task.CompletedTask;

        }

        /// <summary>
        /// TODO : To define Tap on Category Child Command....
        /// </summary>
        /// <param name="meal"></param>
        private void onMealCommandFunc(Meal meal)
        {
            if (meal !=null)
            {
                Navigation.PushAsync(new Views.MealDetailPage(meal), false);
            }
        }
        #endregion
    }
}
