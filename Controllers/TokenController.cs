using jwt_second_version.Data;
using jwt_second_version.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace jwt_second_version.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        public IConfiguration configuration;
        public readonly ApplicationDataContext _context;

        public TokenController(IConfiguration configuration, ApplicationDataContext context)
        {
            this.configuration = configuration;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserInfo user) {
            if (user != null && user.Password != null && user.Username != null) {
                var userInfo = await GetUser(user.Username, user.Password);
                if (userInfo != null)
                {
                    var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim("Id",userInfo.UserId.ToString()),
                new Claim("UserName",userInfo.Username.ToString()),
                new Claim("Password",userInfo.Password.ToString()),
                };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        configuration["Jwt:Issuer"],
                        configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signIn
                        );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else {
                    return BadRequest("Invalid credentials");
                }

            }
            else
            {
                return BadRequest();
            }

        }
       
        private async Task<UserInfo> GetUser(string userName, string password) {
            return await _context.Users.FirstOrDefaultAsync(u => u.Password == password && u.Username == userName);
               
        }
    }
}
