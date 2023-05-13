using System.Collections.Generic;

namespace FoodApp.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }       
        public byte[] ImageArray { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
