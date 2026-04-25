using BusManagement.Api.Interface;
using BusManagement.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BusManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;

        public BusController(IBusService busService)
        {
            _busService = busService;
        }

        // ✅ Get All Buses
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBuses()
        {
            var data = await _busService.GetAllBuses();
            return Ok(data);
        }

        // ✅ Get Bus By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusById(int id)
        {
            var data = await _busService.GetBusById(id);

            if (data == null)
                return NotFound("Bus not found");

            return Ok(data);
        }

        // ✅ Create Bus
        [HttpPost("create")]
        public async Task<IActionResult> CreateBus([FromBody] BusVM model)
        {
            try
            {
                var id = await _busService.CreateBus(model);
                return Ok(new { message = "Bus created successfully", id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ✅ Update Bus
        [HttpPut("update")]
        public async Task<IActionResult> UpdateBus([FromBody] BusVM model)
        {
            if (model.Id == null)
                return BadRequest("Id is required");

            try
            {
                var id = await _busService.UpdateBus(model);
                return Ok(new { message = "Bus updated successfully", id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ✅ Soft Delete Bus
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBus(int id)
        {
            var result = await _busService.DeleteBus(id);

            if (!result)
                return NotFound("Bus not found");

            return Ok(new { message = "Bus deleted successfully" });
        }

        // ✅ Change Status
        [HttpPatch("status")]
        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            var result = await _busService.ChangeStatus(id, status);

            if (!result)
                return NotFound("Bus not found");

            return Ok(new { message = "Status updated successfully" });
        }
    }
}