using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace XamarinApi.Models.Data
{
    public class ProductEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public bool IsPopular { get; set; }
        public string CategoryId { get; set; }
        [NotMapped]
        public byte[] ImageArray { get; set; }
        public ICollection<OrderDetailEntity> OrderDetails { get; set; }
        public ICollection<CartEntity> CartItems { get; set; }
    }
}
