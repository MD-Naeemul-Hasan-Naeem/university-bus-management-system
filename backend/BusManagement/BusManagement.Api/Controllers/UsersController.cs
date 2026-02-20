using BusManagement.Api.Interface;
using BusManagement.Api.ViewModel;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace BusManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsers _users;
        public UsersController(IUsers users)
        {
            _users = users;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var data = await _users.GetAllUsersAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("DeleteUsers/{Id}")]
        public async Task<IActionResult> DeleteUsers(int Id)
        {
            try
            {
                var data = await _users.DeleteUsers(Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("SaveUsers")]
        public async Task<IActionResult> SaveUsers([FromBody] UsersVM model)
        {
            int result = await _users.SaveUsers(model);

            if (result > 0)
                return Ok("User saved successfully");

            return BadRequest("Operation failed");
        }
        [HttpGet("GetUsersById/{Id}")]
        public async Task<IActionResult> GetUsersById(int Id)
        {
            try
            {
                var data = await _users.GetUsersById(Id);
                if (data == null)
                    return NotFound("User not found");

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

