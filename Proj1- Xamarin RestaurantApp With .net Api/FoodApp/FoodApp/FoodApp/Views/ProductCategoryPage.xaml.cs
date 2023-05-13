using FoodApp.Models;
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
	public partial class ProductCategoryPage : ContentPage
	{
		public ProductCategoryPage(ProductModel product)
		{
            InitializeComponent();

            //call api and show data
            catId.Text = product.Title;
        }
	}
}