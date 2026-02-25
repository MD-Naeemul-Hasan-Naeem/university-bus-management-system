using BusManagement.Api.DataContext;
using BusManagement.Api.Helpers;
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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace BusManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersInfoController : ControllerBase
    {
        private readonly IUsersInfo _usersInfo;
        private readonly IUsers _users;
        private readonly JwtHelper _jwtHelper;
        private readonly DapperContext _context;

        public UsersInfoController(IUsersInfo usersInfo,
                                   IUsers users,
                                   JwtHelper jwtHelper,
                                   DapperContext context)
        {
            _usersInfo = usersInfo;
            _users = users;
            _jwtHelper = jwtHelper;
            _context = context;
        }


        // 🔹 GET ALL USERS
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _usersInfo.GetAllAsync();
            return Ok(data);
        }

        // 🔹 GET USER BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _usersInfo.GetByIdAsync(id);
            if (data == null)
                return NotFound("User not found");

            return Ok(data);
        }

        //// 🔹 CREATE AUTH USER (Only Auth Table)
        //[HttpPost("create-auth")]
        //public async Task<IActionResult> CreateAuth(UsersInfoVM model)
        //{
        //    model.PasswordHash = HashPassword(model.Password!);

        //    var newId = await _usersInfo.CreateAuthAsync(model);

        //    return Ok(new { Message = "Auth Created", UserInfoId = newId });
        //}

        // 🔹 UPDATE ROLE
        [HttpPut("update-role/{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] string role)
        {
            await _usersInfo.UpdateRoleAsync(id, role);
            return Ok("Role updated successfully");
        }

        // 🔹 LOCK / UNLOCK USER
        [HttpPut("lock-unlock/{id}")]
        public async Task<IActionResult> LockUnlock(int id, [FromBody] bool isLocked)
        {
            await _usersInfo.LockUnlockAsync(id, isLocked);
            return Ok("User lock status updated");
        }

        // 🔹 SOFT DELETE USER
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _usersInfo.SoftDeleteAsync(id);
            return Ok("User deactivated successfully");
        }

        // 🔹 RESET PASSWORD
        [HttpPut("reset-password/{id}")]
        public async Task<IActionResult> ResetPassword(int id, [FromBody] string newPassword)
        {
            var hash = HashPassword(newPassword);
            await _usersInfo.ResetPasswordAsync(id, hash);

            return Ok("Password reset successfully");
        }

        // 🔐 PASSWORD HASH METHOD
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        //JwtLogin
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVM model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest("Email and password are required");

            // ✅ Trim inputs
            var email = model.Email.Trim();
            var password = model.Password.Trim();

            // ✅ Get user by email
            var user = await _usersInfo.GetByEmailAsync(email);
            if (user == null)
                return Unauthorized("Invalid email or password");

            // ✅ Hash password using same function
            var hashedPassword = HashPassword(password);

            if (user.PasswordHash != hashedPassword)
                return Unauthorized("Invalid email or password");

            if (!user.IsActive)
                return Unauthorized("Account disabled");
            if (user.IsLocked)
                return Unauthorized("Account is locked");

            // ✅ Generate JWT
            var token = _jwtHelper.GenerateToken(
                user.Id,
                user.Email,
                user.Role
            );

            return Ok(new
            {
                Token = token,
                Role = user.Role
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var email = model.Email.Trim();
            var password = model.Password.Trim();

            // ✅ Check duplicate email
            var existingUser = await _usersInfo.GetByEmailAsync(email);
            if (existingUser != null)
                return BadRequest(new { message = "Email already exists" });

            using var connection = _context.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                var passwordHash = HashPassword(password);

                // 1️⃣ Create Auth record
                int userInfoId = await _usersInfo.CreateAuthAsync(
                    new UsersInfoVM
                    {
                        Email = email,
                        PasswordHash = passwordHash,
                        Role = model.Role ?? "User",
                        IsActive = true,
                        IsLocked = false
                    },
                    connection,
                    transaction
                );

                // 2️⃣ Create Profile record
                await _users.CreateProfileAsync(
                    new UsersVM
                    {
                        UserInfoId = userInfoId,
                        FullName = model.FullName,
                        Phone = model.Phone,
                        Department = model.Department,
                        StudentId = model.StudentId,
                        EmployeeId = model.EmployeeId
                    },
                    connection,
                    transaction
                );

                transaction.Commit();

                return Ok(new { message = "Registration successful" });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return BadRequest(new { message = "Registration failed", error = ex.Message });
            }
        }

    }
}
