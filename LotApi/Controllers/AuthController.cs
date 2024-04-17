using LotApi.Data;
using LotApi.Dto;
using LotApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using LotApi.Mappers;
using Microsoft.Extensions.Configuration;


namespace LotApi.Controllers
{
    
    [ApiController]
    [Route("api/user")]
    public class AuthController : ControllerBase
    {
        
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;

        public AuthController(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult<UserData> Register(UserDto request)
        {
            if (_dataContext.UserData.Any(x => x.Username == request.Username))
            {
                return BadRequest("User already exists.");
            }

            UserData user = new UserData();

            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);

            user.Username = request.Username;
            user.PasswordHash = passwordHash;

            _dataContext.UserData.Add(user);
            _dataContext.SaveChanges();

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<UserData> Login(UserDto request)
        {
            var user = _dataContext.UserData.FirstOrDefault(x => x.Username == request.Username);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(UserData user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
