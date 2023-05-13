namespace FoodApp.Models
{
    public class OrderDetailModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
        public int OrderId { get; set; }
        public OrderModel Order { get; set; }
        public double OrderTotal { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
