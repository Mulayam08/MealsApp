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
    public class MealDetailPageViewModel : BaseViewModel
    {
        #region Variables  
        #endregion

        #region Constructor
        public MealDetailPageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(OnBackCommandAsync); 
            }
            catch (Exception ex)
            { }
        }

        #endregion

        #region Command  
        public Command BackCommand { get; set; } 
        public Command CategoryChildTapCommand { get; set; }

        #endregion

        #region Properties
         
        private Meal _SelectedMeal;
        public Meal SelectedMeal
        {
            get { return _SelectedMeal; }
            set
            {
                if (_SelectedMeal != value)
                {
                    _SelectedMeal = value;
                    OnPropertyChanged("SelectedMeal");
                }
            }
        }
        private MealDetails _MealDetail;
        public MealDetails MealDetail
        {
            get { return _MealDetail; }
            set
            {
                if (_MealDetail != value)
                {
                    _MealDetail = value;
                    OnPropertyChanged("MealDetail");
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
        public async Task GetMealDetails()
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
                            await _businessCode.GetMealDetails(SelectedMeal.idMeal,
                            async (obj) =>
                            {
                                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                {
                                    var requestList = obj as MealsDetailModel;
                                    if (requestList != null)
                                    {
                                        if (requestList.meals != null)
                                        {
                                            if (requestList.meals.Count > 0)
                                            {
                                                MealDetail = requestList.meals[0];
                                            }
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

        #endregion
    }
}
 