using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace XamarinApi.Models.Data
{
    public class CategoryEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public byte[] ImageArray { get; set; }
        public ICollection<ProductEntity> Products { get; set; }
    }
}
