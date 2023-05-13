using AuthenticationPlugin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using XamarinApi.Models.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using XamarinApi.Models;
using System.Data;

namespace XamarinApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private AppDbContext _appDbContext;
        private IConfiguration _configuration;
        private readonly AuthService _authService;
        public AccountsController(
            AppDbContext appDbContext,
            IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
            _authService = new AuthService(_configuration);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(AuthModel auth)
        {
            UserEntity user = new UserEntity()
            {
                Name = auth.Name,
                Email = auth.Email,
                Password = auth.Password,
                Role = auth.Role
            };
            var remailAlreadyExists = _appDbContext.Users.Any(x => x.Email == user.Email);

            if (remailAlreadyExists)
            {
                return BadRequest("User Exists");
            }

            //Add user to db
            user.Password = SecurePasswordHasherHelper.Hash(user.Password);
            user.Role = "User";
            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AuthModel auth)
        {
            UserEntity user = new UserEntity()
            {
                Email = auth.Email.ToLower(),
                Password = auth.Password
            };

            var userResult = _appDbContext.Users.FirstOrDefault(x => x.Email.ToLower() == user.Email.ToLower());
            if (userResult == null)
                return StatusCode(StatusCodes.Status404NotFound);
            if (!SecurePasswordHasherHelper.Verify(user.Password, userResult.Password))
                return Unauthorized();

            //if valid then create claims
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, userResult.Role),
            };
            
            var token = _authService.GenerateAccessToken(claims);

            return new ObjectResult(new
            {
                Access_Token = token.AccessToken,
                TokenType = token.TokenType,
                UserId = userResult.Id,
                UserName = userResult.Name,
                Role = userResult.Role,
                ExperiesIn = token.ExpiresIn.ToString(),
                CreationTime = token.ValidFrom.ToString(),
                ExpirationTime = token.ValidTo.ToString(),
            });

        }
    }
}
