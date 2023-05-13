using FoodApp.Models;
using FoodApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
            Email.Text = "Admin@admin.com";
            Password.Text = "admin";
        }

        private async void LoginBtn_Clicked(object sender, EventArgs e)
        {
            if (Email.Text == null || Password.Text == null)
            {
                await DisplayAlert("Alert!!", "Email Id / Password Required", "Ok");
                return;
            }

            //Login through Api
            UserModel userModel = new UserModel()
            {
                Email = Email.Text.ToLower(),
                Password = Password.Text
            };
            var result = await ApiService.Login(userModel);
            if (result)
            {
                await DisplayAlert("Success", "User Signeded-In Successfully", "Ok");

                //makes this page as first route and back button or back route doesnt work.
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                await DisplayAlert("Alert!!", "Error while User Sign In", "Ok");
            }
        }
    }
}