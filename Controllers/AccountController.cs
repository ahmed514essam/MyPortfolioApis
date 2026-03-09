using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MyPortfolioApis.Dtos;
using MyPortfolioApis.Models;

namespace MyPortfolioApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        public AccountController(UserManager<User> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUser(NewUserDto user)
        {
            if (ModelState.IsValid)
            {
                User newUser = new()
                {
                    UserName = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.Phone,
                };
                IdentityResult result = await userManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    return Ok("Success");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return BadRequest(ModelState);
        }


        [HttpPost]
        public async Task<IActionResult> LogIn(LoginDto login)
        {
            if (ModelState.IsValid)
            {
                User? user = await userManager.FindByEmailAsync(login.Email);
                if (user != null)
                {
                    if (await userManager.CheckPasswordAsync(user, login.Password))
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Email, user.Email));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));


                        var roles = await userManager.GetRolesAsync(user);
                        foreach (var item in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item.ToString()));
                        }
                        var key
                        = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
                        var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            claims: claims,
                            issuer: configuration["Jwt:Issuer"],
                            audience: configuration["Jwt:Audience"],
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: sc
                            );
                        var _token = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                        };
                        return Ok(_token);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email or Password!");
                }
            }
            return BadRequest(ModelState);
        }







    }
}
