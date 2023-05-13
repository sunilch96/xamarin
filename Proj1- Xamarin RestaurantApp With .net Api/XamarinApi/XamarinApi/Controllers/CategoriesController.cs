using AuthenticationPlugin;
using ImageUploader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XamarinApi.Models.Data;

namespace XamarinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        AppDbContext _appDbContext;        
        public CategoriesController( AppDbContext appDbContext )
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var categories = _appDbContext.Categories.ToList();
            return Ok(categories);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _appDbContext.Categories.FirstOrDefault(x => x.Id == id);
            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Post([FromBody] CategoryEntity category)
        {
            var stream = new MemoryStream(category.ImageArray);
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";
            var folder = "wwwroot";
            var response = FilesHelper.UploadImage(stream, file, folder);
            if (!response)
            {
                return BadRequest(response);
            }
            category.ImageUrl = file;
            _appDbContext.Categories.Add(category);
            _appDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Post(int id, [FromBody] CategoryEntity category)
        {
            var categoryEntity = _appDbContext.Categories.FirstOrDefault(x => x.Id == id);
            if (categoryEntity == null)
            {
                return NotFound("Category not found.");
            }

            var stream = new MemoryStream(category.ImageArray);
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";
            var folder = "wwwroot";
            var response = FilesHelper.UploadImage(stream, file, folder);
            if (!response)
            {
                return BadRequest(response);
            }
            categoryEntity.ImageUrl = file;
            categoryEntity.Title = category.Title;
            _appDbContext.Update(categoryEntity);
            _appDbContext.SaveChanges();
            return Ok("Category Updated");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categoryEntity = _appDbContext.Categories.FirstOrDefault(x => x.Id == id);
            if (categoryEntity == null)
            {
                return NotFound("Category not found.");
            }
            _appDbContext.Categories.Remove(categoryEntity);
            _appDbContext.SaveChanges();
            return Ok("Category Deleted");
        }
    }
}
