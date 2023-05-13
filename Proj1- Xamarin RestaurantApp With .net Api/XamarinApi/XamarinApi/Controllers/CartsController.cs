using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XamarinApi.Models.Data;

namespace XamarinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        AppDbContext _appDbContext;
        public CartsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("{userId}")]
        public IActionResult Get(int userId)
        {            
            var user = _appDbContext.Users.FirstOrDefault(x => x.Id == userId);
            if(user == null)
            {
                return NotFound();
            }

            var cartItem = from ct in _appDbContext.Cart.Where(x => x.CustomerId == userId)
                           join pr in _appDbContext.Products
                           on ct.ProductId equals pr.Id
                           select new
                           {
                               Id = ct.Id,
                               Price = pr.Price,
                               TotalAmount = ct.TotalAmount,
                               Quantity = ct.Quantity,
                               ProductName = pr.Title,
                               ProductDescription = pr.Description,
                           };

            return Ok(cartItem);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CartEntity cart)
        {
            var cartRes = _appDbContext.Cart
                .FirstOrDefault(x => x.ProductId == cart.ProductId 
                && x.CustomerId == cart.CustomerId);
            if(cartRes == null)
            {
                //add new cart
                _appDbContext.Cart.Add(cart);
                _appDbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            };

            //modify
            cartRes.Quantity += cart.Quantity;
            cartRes.TotalAmount = cartRes.Price * cartRes.Quantity;
            _appDbContext.Cart.Update(cart);
            _appDbContext.SaveChanges();
            return Ok("Category Updated");
        }

        [HttpGet("[action]/{userId}")]
        public IActionResult TotalItems(int userId)
        {
            var cartItemsCount = _appDbContext.Cart.Where(x=> x.CustomerId == userId).Sum(y => y.Quantity);
            return Ok(new { TotalItems = cartItemsCount });
        }

        [HttpGet("[action]/{userId}")]
        public IActionResult TotalAmount(int userId)
        {
            var cartItemsTotalAmount = _appDbContext.Cart.Where(x => x.CustomerId == userId)
                .Sum(x => x.TotalAmount);
            return Ok(new { TotalAmount = cartItemsTotalAmount });
        }

        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            var cartItemsCount = _appDbContext.Cart.Where(x => x.CustomerId == userId);
            _appDbContext.Cart.RemoveRange(cartItemsCount);
            _appDbContext.SaveChanges();
            return Ok();
        }
    }
}
