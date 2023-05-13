using ImageUploader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XamarinApi.Models.Data;

namespace XamarinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        AppDbContext _appDbContext;
        public ProductsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_appDbContext.Products.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _appDbContext.Products.FirstOrDefault(x => x.Id == id);
            return Ok(product);
        }

        [HttpGet("[action]/{categoryId}")]
        public IActionResult GetProductByCategoryId(string categoryId)
        {
            var products = _appDbContext.Products
                .Where(x => x.CategoryId == categoryId)
                .ToList();

            return Ok(products);
        }

        [HttpGet("[action]")]
        public IActionResult PopularProducts()
        {
            var products = _appDbContext.Products
                .Where(x => x.IsPopular == true)
                .ToList();
            return Ok(products);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Post([FromBody] ProductEntity product)
        {
            var stream = new MemoryStream(product.ImageArray);
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";
            var folder = "wwwroot";
            var response = FilesHelper.UploadImage(stream, file, folder);
            if (!response)
            {
                return BadRequest(response);
            }

            product.ImageUrl = file;
            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Post(int id, [FromBody] ProductEntity product)
        {
            var productEntity = _appDbContext.Products.FirstOrDefault(x => x.Id == id);
            if (productEntity == null)
            {
                return NotFound("Product not found.");
            }
            var stream = new MemoryStream(product.ImageArray);
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";
            var folder = "wwwroot";
            var response = FilesHelper.UploadImage(stream, file, folder);
            if (!response)
            {
                return BadRequest(response);
            }

            productEntity.ImageUrl = file;
            productEntity.Title= product.Title;
            _appDbContext.Products.Update(productEntity);
            _appDbContext.SaveChanges();
            return Ok("Product Updated");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var productEntity = _appDbContext.Products.FirstOrDefault(x => x.Id == id);
            if (productEntity == null)
            {
                return NotFound("Product not found.");
            }
            _appDbContext.Products.Remove(productEntity);
            return Ok("Product Deleted");
        }
    }
}
