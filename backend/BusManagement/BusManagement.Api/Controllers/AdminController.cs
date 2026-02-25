using BusManagement.Api.DataContext;
using BusManagement.Api.Interface;
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
        private readonly IAdminService _adminService;


        public AdminController(DapperContext context, IAdminService adminService)
        {
            _context = context;
            _adminService = adminService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(AdminDashboardVM model)
        {
            var result = await _adminService.CreateUserAsync(model);

            if (!result)
                return BadRequest("Error creating user");

            return Ok("User created successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var data = await _adminService.GetDashboardData();
            return Ok(data);
        }
    }
}
