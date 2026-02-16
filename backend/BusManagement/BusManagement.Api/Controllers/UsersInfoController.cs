using BusManagement.Api.Interface;
using BusManagement.Api.ViewModel;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace BusManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersInfoController : ControllerBase
    {
        private readonly IUsersInfo _usersInfo;

        private readonly IConfiguration _config;
        public UsersInfoController(IUsersInfo usersInfo, IConfiguration config)
        {
            _usersInfo = usersInfo;
            _config = config;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await _usersInfo.GetAllUsersAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("save")]
        public async Task<IActionResult> Save(UsersInfoVM model)
        {
            try
            {

                var data = await _usersInfo.Save(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody]UsersInfoVM model)
        {
            try
            {
                if (model == null || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
                {
                    return BadRequest(new
                    {
                        message = "Email and Password are required."
                    });
                }

                var user = await _usersInfo.login(model);

                if (user == null)
                {
                    return Unauthorized(new
                    {
                        message = "Invalid email or password."
                    });
                }

                //// ====== VERIFY PASSWORD ======
                //var passwordHasher = new PasswordHasher<UsersInfoVM>();
                //var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

                //if (result == PasswordVerificationResult.Failed)
                //{
                //    return Unauthorized(new { message = "Invalid email or password." });
                //}

                // ====== JWT GENERATION ======
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(ClaimTypes.Name, user.Email.ToString()),
                new Claim(ClaimTypes.Role, user.UserType.ToString()) // <-- Role from DB
                };

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: creds
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


                return Ok(new
                {
                    message = "Login successful",
                    token = tokenString,
                    role = user.UserType,
                    data = user
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // ✅ Admin-only endpoint
        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult AdminOnly()
        {
            return Ok("This is admin only");
        }

        [HttpGet("generate-hash")]
        public IActionResult GenerateHash(string password)
        {
            var hasher = new PasswordHasher<object>();
            var hash = hasher.HashPassword(null, password);
            return Ok(hash);
        }



    }
}
