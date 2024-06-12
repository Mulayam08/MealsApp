﻿using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using System.Diagnostics;

namespace MealApp
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            #region Initialize Packages  
            UserDialogs.Init(this);
            #endregion
        } 
    }
}
