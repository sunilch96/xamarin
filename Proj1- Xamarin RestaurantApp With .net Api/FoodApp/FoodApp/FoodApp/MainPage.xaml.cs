using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            //local storage
            Preferences.Set("UserName", UserName.Text);
            label1.Text = Preferences.Get("UserName", string.Empty);
        }
    }
}
