using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XamarinApi.Models.Data;

namespace XamarinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        AppDbContext _appDbContext;
        public OrdersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public IActionResult Post([FromBody] OrderEntity order)
        {
            order.IsCompleted = false;
            order.DateTime = DateTime.Now;
            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();

            var cartItems = _appDbContext.Cart.Where(x => x.CustomerId == order.UserId);
            foreach(var item in cartItems)
            {
                var orderDetails = new OrderDetailEntity()
                {
                    Price = item.Price,
                    OrderTotal = item.TotalAmount,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,
                    OrderId = order.Id
                };
                _appDbContext.OrderDetails.Add(orderDetails);
            }

            _appDbContext.SaveChanges();
            _appDbContext.Cart.RemoveRange(cartItems);
            _appDbContext.SaveChanges();

            return Ok(new
            {
                OrderId = order.Id
            });
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("[action]")]
        public IActionResult PendingOrders()
        {
            var orders = _appDbContext.Orders.Where(x => x.IsCompleted == false);
            return Ok(orders);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult CompletedOrders()
        {
            var orders = _appDbContext.Orders.Where(x => x.IsCompleted == true);
            return Ok(orders);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult OrderDetails(int orderId)
        {
            var orders = _appDbContext.Orders.Where(x => x.Id == orderId)
                .Include(y=>y.OrderDetails)
                .ThenInclude(p=>p.Product);
            return Ok(orders);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult TotalOrders()
        {
            var orders = _appDbContext.Orders.Where(x => x.IsCompleted == false).Count(y => y.IsCompleted == false);
            return Ok(orders);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]/{userId}")]
        public IActionResult OrdersByUser(int userId)
        {
            var orders = _appDbContext.Orders.Where(x => x.UserId == userId).OrderByDescending(x => x.DateTime);
            return Ok(orders);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]/{orderId}")]
        public IActionResult MarkOrderCompleted(int orderId, [FromBody] OrderEntity order)
        {
            var orders = _appDbContext.Orders.Find(orderId);
            if(orders == null)
            {
                return NotFound("Not Found");
            }

            orders.IsCompleted = order.IsCompleted;
            _appDbContext.SaveChanges();
            return Ok("Order Completed");
        }
    }
}
