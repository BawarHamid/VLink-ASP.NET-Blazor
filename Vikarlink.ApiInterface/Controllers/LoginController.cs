using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vikarlink.ApiInterface.Database;
using Vikarlink.ApiInterface.Repositories.VikarContract;
using Vikarlink.Shared;

namespace Vikarlink.ApiInterface.Controllers
{
    [Route("/LoginApi")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IVikarRepository _vikarRepo;

        public LoginController(IConfiguration configuration, UserManager<UserEntity> userManager,
          RoleManager<IdentityRole> roleManager, IVikarRepository vikarRepo)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _vikarRepo = vikarRepo;
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {

            var user = await _userManager.FindByNameAsync(request.Username);
            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return BadRequest("Invalid username or password");
            }

            var authClaims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, user.Id),
              new Claim(ClaimTypes.Name, user.UserName),
              new Claim("vikarIdentifier", _vikarRepo.FindVikarByUsername(request.Username).Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                authClaims.Add(new(ClaimTypes.Role, role));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [HttpGet]
        public string GetId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            Console.WriteLine("ID");
            if (identity == null)
            {
                foreach (var claim in identity.Claims)
                    {
                    Console.WriteLine(claim.Type);
                     }
            }
            return "6";
        }
    }




}

