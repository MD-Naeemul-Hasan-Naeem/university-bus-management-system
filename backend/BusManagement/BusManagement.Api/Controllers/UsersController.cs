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

        // 🔹 GET PROFILE BY USERINFOID
        [HttpGet("profile/{userInfoId}")]
        public async Task<IActionResult> GetProfile(int userInfoId)
        {
            var data = await _users.GetProfileByUserInfoIdAsync(userInfoId);

            if (data == null)
                return NotFound("Profile not found");

            return Ok(data);
        }

        //// 🔹 CREATE PROFILE
        //[HttpPost]
        //public async Task<IActionResult> CreateProfile(UsersVM model)
        //{
        //    await _users.CreateProfileAsync(model);
        //    return Ok("Profile created successfully");
        //}

        // 🔹 UPDATE PROFILE
        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UsersVM model)
        {
            await _users.UpdateProfileAsync(model);
            return Ok("Profile updated successfully");
        }

        // 🔹 DELETE PROFILE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            await _users.DeleteProfileAsync(id);
            return Ok("Profile deleted successfully");
        }
    }
}

