using FoodApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FoodApp.Services
{
    public static class ApiService
    {
        public async static Task<bool> Register(UserModel user)
        {
            HttpClientHandler httpClientHandler = RemoteHandler();
            var httpClient = new HttpClient(httpClientHandler);
            var postData = new StringContent(   JsonConvert.SerializeObject(user), 
                                                Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(Constants.RegisterApiUrl, postData);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async static Task<bool> Login(UserModel user)
        {
            HttpClientHandler httpClientHandler = RemoteHandler();
            var httpClient = new HttpClient(httpClientHandler);
            var postData = new StringContent(JsonConvert.SerializeObject(user),
                                                Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(Constants.LoginApiUrl, postData);
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var tokenData = JsonConvert.DeserializeObject<TokenModel>(responseJson);
                Preferences.Set(nameof(TokenModel.Access_Token), tokenData.Access_Token);
                Preferences.Set(nameof(TokenModel.UserId), tokenData.UserId);
                Preferences.Set(nameof(TokenModel.UserName), tokenData.UserName);
                Preferences.Set(nameof(TokenModel.Role), tokenData.Role);
                return true;
            }
            return false;
        }

        public async static Task<List<CategoryModel>> GetAllCategory()
        {
            HttpClientHandler httpClientHandler = RemoteHandler();
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue
                ("bearer", Preferences.Get(nameof(TokenModel.Access_Token), string.Empty));

            var response = await httpClient.GetStringAsync(Constants.GetAllCategoryApiUrl);
            return JsonConvert.DeserializeObject<List<CategoryModel>>(response);
        }

        public async static Task<ProductModel> GetProductById(int productId)
        {
            HttpClientHandler httpClientHandler = RemoteHandler();
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue
                ("bearer", Preferences.Get(nameof(TokenModel.Access_Token), string.Empty));

            var response = await httpClient.GetStringAsync(Constants.ProductsApiUrl + "/" + productId);
            return JsonConvert.DeserializeObject<ProductModel>(response);
        }

        public async static Task<List<ProductModel>> GetAllProduct()
        {
            HttpClientHandler httpClientHandler = RemoteHandler();
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue
                ("bearer", Preferences.Get(nameof(TokenModel.Access_Token), string.Empty));

            var response = await httpClient.GetStringAsync(Constants.ProductsApiUrl);
            return JsonConvert.DeserializeObject<List<ProductModel>>(response);
        }

        public async static Task<List<ProductModel>> GetProductByCategoryId(int categoryId)
        {
            HttpClientHandler httpClientHandler = RemoteHandler();
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue
                ("bearer", Preferences.Get(nameof(TokenModel.Access_Token), string.Empty));

            var response = await httpClient.GetStringAsync
                (Constants.GetProductByCategoryIdApiUrl + "/" + categoryId);
            return JsonConvert.DeserializeObject<List<ProductModel>>(response);
        }

        public async static Task<List<ProductModel>> GetAllPopularProducts()
        {
            HttpClientHandler httpClientHandler = RemoteHandler();
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue
                ("bearer", Preferences.Get(nameof(TokenModel.Access_Token), string.Empty));

            var response = await httpClient.GetStringAsync(Constants.GetAllPopularProductsApiUrl);
            return JsonConvert.DeserializeObject<List<ProductModel>>(response);
        }

        //Cart Api
        #region Cart Api
        public async static Task<bool> AddItemsToCart(CartModel cart)
        {
            HttpClientHandler httpClientHandler = RemoteHandler();
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue
                ("bearer", Preferences.Get(nameof(TokenModel.Access_Token), string.Empty));

            var postData = new StringContent(JsonConvert.SerializeObject(cart),
                                                Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(Constants.CartsApiUrl, postData);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async static Task<List<CartModel>> GetCartItemsByUserId(int UserId)
        {
            HttpClientHandler httpClientHandler = RemoteHandler();
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue
                ("bearer", Preferences.Get(nameof(TokenModel.Access_Token), string.Empty));
            var response = await httpClient.GetStringAsync(Constants.CartsApiUrl + "/" + UserId);
            return JsonConvert.DeserializeObject<List<CartModel>>(response);
        }

        public async static Task<bool> DeleteCartItemsByUserId(int UserId)
        {
            HttpClientHandler httpClientHandler = RemoteHandler();
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue
                ("bearer", Preferences.Get(nameof(TokenModel.Access_Token), string.Empty));
            var response = await httpClient.DeleteAsync(Constants.CartsApiUrl + "/" + UserId);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async static Task<CartModel> GetCartTotalItemsByUserId(int UserId)
        {
            HttpClientHandler httpClientHandler = RemoteHandler();
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue
                ("bearer", Preferences.Get(nameof(TokenModel.Access_Token), string.Empty));
            var response = await httpClient.GetStringAsync(Constants.CartTotalItemsApiUrl + "/" + UserId);
            return JsonConvert.DeserializeObject<CartModel>(response);
        }

        public async static Task<CartModel> GetCartTotalAmountByUserId(int UserId)
        {
            HttpClientHandler httpClientHandler = RemoteHandler();
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue
                ("bearer", Preferences.Get(nameof(TokenModel.Access_Token), string.Empty));
            var response = await httpClient.GetStringAsync(Constants.CartTotalAmountApiUrl + "/" + UserId);
            return JsonConvert.DeserializeObject<CartModel>(response);
        }
        #endregion

        //Order
        #region Order
        public async static Task<OrderModel> AddOrder(OrderModel cart)
        {
            HttpClientHandler httpClientHandler = RemoteHandler();
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue
                ("bearer", Preferences.Get(nameof(TokenModel.Access_Token), string.Empty));

            var postData = new StringContent(JsonConvert.SerializeObject(cart),
                                                Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(Constants.OrdersApiUrl, postData);
            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OrderModel>(jsonData);
        }

        public async static Task<List<OrderModel>> GetOrderByUserId(int UserId)
        {
            HttpClientHandler httpClientHandler = RemoteHandler();
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue
                ("bearer", Preferences.Get(nameof(TokenModel.Access_Token), string.Empty));
            var response = await httpClient.GetStringAsync(Constants.OrdersByUserApiUrl + "/" + UserId );
            return JsonConvert.DeserializeObject<List<OrderModel>>(response);
        }

        public async static Task<List<OrderModel>> GetOrderDetails(int OrderId)
        {
            HttpClientHandler httpClientHandler = RemoteHandler();
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue
                ("bearer", Preferences.Get(nameof(TokenModel.Access_Token), string.Empty));
            var response = await httpClient.GetStringAsync(Constants.OrderDetailsApiUrl + "/" + OrderId);
            return JsonConvert.DeserializeObject<List<OrderModel>>(response);
        }
        #endregion

        //http handler
        private static HttpClientHandler RemoteHandler()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback +=
                (message, certificate, chain, SslPolicyErrors)=> true;
            return httpClientHandler;
        }
    }
}
