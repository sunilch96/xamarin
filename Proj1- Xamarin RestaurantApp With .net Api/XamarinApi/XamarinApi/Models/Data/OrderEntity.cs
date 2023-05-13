using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace XamarinApi.Models.Data
{
    public class OrderEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public double Total { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public ICollection<OrderDetailEntity> OrderDetails { get; set; }
    }
}

