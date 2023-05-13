using System;
using System.Collections.Generic;

namespace FoodApp.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public double Total { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public List<OrderDetailModel> OrderDetails { get; set; }
    }
}

