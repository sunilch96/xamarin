
using System.Collections.Generic;

namespace FoodApp.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public bool IsPopular { get; set; }
        public string CategoryId { get; set; }
       
        public byte[] ImageArray { get; set; }
        public List<OrderDetailModel> OrderDetails { get; set; }
        public List<CartModel> CartItems { get; set; }
    }
}
