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
    public partial class SignUpPage : ContentPage
    {        
        public SignUpPage()
        {
            InitializeComponent();            
        }

        private async void SignUpBtn_Clicked(object sender, EventArgs e)
        {
            if (!Password.Text.Equals(ConfirmPassword.Text))
            {
                await DisplayAlert("Alert!!", "Password Mismatch.", "Ok");
                return;
            }

            //register through Api
            UserModel userModel = new UserModel()
            {
                Name = Name.Text,
                Email = Email.Text,
                Password = Password.Text,
                Role="User"
            };
            var result = await ApiService.Register(userModel);
            if (result)
            {
                await DisplayAlert("Success", "User Registered Successfully", "Ok");
                await Navigation.PushModalAsync(new SignInPage());
            }
            else
            {
                await DisplayAlert("Alert!!", "Error while User Registration", "Ok");
            }
        }

        private void LoginBtn_Clicked(object sender, EventArgs e)
        {

        }

        private async void LoginBtn_PG_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SignInPage());
        }
    }
}