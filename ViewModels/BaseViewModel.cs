using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealApp.BusinessCode; 
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace MealApp.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty, NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        public bool IsNotBusy => !IsBusy;

        private INavigation _Navigation; 

        public IBusinessCode _businessCode; 
        public Command BacksCommand { get; set; }
        public Command CloseCommand { get; set; }
        public INavigation Navigation
        {
            get { return _Navigation; }
            set
            {
                if (_Navigation != value)
                {
                    _Navigation = value;
                    OnPropertyChanged("Navigation");
                }
            }
        }  
        public BaseViewModel(INavigation navigation = null)
        {
            try
            {
                Navigation = navigation;
                BacksCommand = new Command(OnBacksAsync);
                CloseCommand = new Command(ClosePopupAsync);
                _businessCode = Ioc.Default.GetService<IBusinessCode>(); 

            }
            catch (Exception ex)
            { }
        }
        public void Busy() => IsBusy = true;
        public void NotBusy() => IsBusy = false;
        private void ClosePopupAsync(object obj)
        {
            this.PopAsync();
        }
         
        /// <summary>
        /// TODO : To Navigate To Back Page...
        /// </summary>
        public async void OnBacksAsync()
        {
            await PopAsync();
        }
        public Acr.UserDialogs.IUserDialogs UserDialog
        {
            get
            {
                return Acr.UserDialogs.UserDialogs.Instance;
            }
        }
        public async Task PushAsync(Page page)
        {
            if (Navigation != null)
                await Navigation.PushAsync(page);
        } 
        public async Task PopAsync()
        {
            if (Navigation != null)
                await Navigation.PopAsync();
        } 
    }
}
