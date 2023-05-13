using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodApp.Models
{
    public static class Constants
    {
        //Api
        public static readonly string BaseApiUrl = GetBaseUrl();

        public static string RegisterApiUrl = BaseApiUrl + "/api/Accounts/Register";
        public static string LoginApiUrl = BaseApiUrl + "/api/Accounts/Login";

        //Categories
        public static string GetAllCategoryApiUrl = BaseApiUrl + "/api/Categories";

        //Products
        public static string ProductsApiUrl = BaseApiUrl + "/api/Products";
        public static string GetProductByCategoryIdApiUrl = BaseApiUrl  + "/api/Products/GetProductByCategoryId";
        public static string GetAllPopularProductsApiUrl = BaseApiUrl  + "/api/Products/PopularProducts";

        //Cart
        public static string CartsApiUrl = BaseApiUrl + "/api/Carts";
        public static string CartTotalItemsApiUrl = BaseApiUrl + "/api/Carts/TotalItems";
        public static string CartTotalAmountApiUrl = BaseApiUrl + "/api/Carts/TotalAmount";

        //orders
        public static string OrdersApiUrl = BaseApiUrl + "/api/Orders";
        public static string PendingOrdersApiUrl = BaseApiUrl + "/api/Orders/PendingOrders";
        public static string CompletedOrdersApiUrl = BaseApiUrl + "/api/Orders/CompletedOrders";
        public static string OrderDetailsApiUrl = BaseApiUrl + "/api/Orders/OrderDetails";
        public static string TotalOrdersApiUrl = BaseApiUrl + "/api/Orders/TotalOrders";
        public static string OrdersByUserApiUrl = BaseApiUrl + "/api/Orders/OrdersByUser";
        public static string MarkOrderCompletedApiUrl = BaseApiUrl + "/api/Orders/MarkOrderCompleted";
        
        public static string GetBaseUrl()
        {
            if (Device.RuntimePlatform == Device.Android)
                return "https://10.0.2.2:7112";

            else
                return "https://localhost:7112";
        }
        public static TokenModel GetTokenDetails()
        {
            TokenModel token = new TokenModel();
            token.Access_Token =  Preferences.Get(nameof(TokenModel.Access_Token), string.Empty);
            token.UserId = Preferences.Get(nameof(TokenModel.UserId), 0);
            token.UserName = Preferences.Get(nameof(TokenModel.UserName), string.Empty);
            return token;
        }

    }
}
