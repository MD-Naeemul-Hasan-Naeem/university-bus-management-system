using BusManagement.Api.DataContext;
using BusManagement.Api.ViewModel;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;

namespace BusManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AdminController : ControllerBase
    {
        private readonly DapperContext _context;

        public AdminController(DapperContext context)
        {
            _context = context;
        }

        [HttpPost("create-user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] AdminDashboardVM model)
        {
            using var connection = _context.CreateConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                // 🔐 Insert Auth
                var authParams = new DynamicParameters();
                authParams.Add("@flag", 3);
                authParams.Add("@Email", model.Email);
                authParams.Add("@PasswordHash", HashPassword(model.Password));
                authParams.Add("@Role", model.Role);

                var userInfoId = await connection.ExecuteScalarAsync<int>(
                    "SP_UsersInfo",
                    authParams,
                    transaction,
                    commandType: CommandType.StoredProcedure);

                // 👤 Insert Profile
                var profileParams = new DynamicParameters();
                profileParams.Add("@flag", 3);
                profileParams.Add("@UserInfoId", userInfoId);
                profileParams.Add("@Name", model.Name);
                profileParams.Add("@Phone", model.Phone);
                profileParams.Add("@Department", model.Department);
                profileParams.Add("@StudentId", model.StudentId);
                profileParams.Add("@EmployeeId", model.EmployeeId);

                await connection.ExecuteAsync(
                    "SP_Users",
                    profileParams,
                    transaction,
                    commandType: CommandType.StoredProcedure);

                transaction.Commit();

                return Ok("User created successfully");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return BadRequest("Error creating user");
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(sha256.ComputeHash(bytes));
        }
    }
}
