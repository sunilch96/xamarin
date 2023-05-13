using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace XamarinApi.Models.Data
{
    public class OrderDetailEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; }
        public double OrderTotal { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
