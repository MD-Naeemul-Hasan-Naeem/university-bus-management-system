using BusManagement.Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var data = await _adminService.GetDashboardData();
            return Ok(data);
        }
    }
}
