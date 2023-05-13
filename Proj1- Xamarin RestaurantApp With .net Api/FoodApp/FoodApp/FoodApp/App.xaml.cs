﻿using FoodApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new SignUpPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
