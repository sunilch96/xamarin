using FoodApp.Models;
using FoodApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<ProductModel> Products;
        public ObservableCollection<CategoryModel> Categories;
        public MainPage()
        {
            InitializeComponent();
            Products = new ObservableCollection<ProductModel>();
            Categories = new ObservableCollection<CategoryModel>();
            AllFunctions();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            GetCartCount();
        }

        private async void GetCartCount()
        {
            var cartItems = await ApiService
                .GetCartTotalItemsByUserId(Constants.GetTokenDetails().UserId);
            TotalItemsLbl.Text = cartItems.TotalItems.ToString();
        }
        private void AllFunctions()
        {
            GetPopularProducts();
            GetCategories();
            GetCartCount();
            userNamelbl.Text = Constants.GetTokenDetails().UserName;
            roleNamelbl.Text = $"({Constants.GetTokenDetails().Role})";
        }
        async void GetPopularProducts()
        {
            var popularProducts = await ApiService.GetAllPopularProducts();
            foreach (var items in popularProducts)
            {
                Products.Add(items);
            }
            productsCollection.ItemsSource = Products;
        }

        async void GetCategories()
        {
            var categoriesList = await ApiService.GetAllCategory();
            foreach (var items in categoriesList)
            {
                Categories.Add(items);
            }
            categoriesCollection.ItemsSource = Categories;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            AllFunctions();
        }

        private async void ImgMenu_Clicked(object sender, EventArgs e)
        {
            sideMenu.IsVisible= true;
            await sideMenu.TranslateTo(0, 0, 400, Easing.Linear);
        }

        private async void CloseMenuTap_Tapped(object sender, EventArgs e)
        {
            await sideMenu.TranslateTo(-250, 0, 400, Easing.Linear);
            sideMenu.IsVisible = false;
        }

        private void productsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSel = e.CurrentSelection.FirstOrDefault() as ProductModel;
            if (currentSel ==null)
            {
                return;
            }
            Navigation.PushAsync(new ProductCategoryPage(currentSel));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}