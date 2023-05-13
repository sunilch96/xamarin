namespace FoodApp.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotalAmount { get; set; }
        public double TotalItems { get; set; }
        public int CustomerId { get; set; }
    }
}
