using FMA.BLL.Services.Interfaces;
using FMA.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FMA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bookingService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingDTO dto)
        {
            var result = await _bookingService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBookingDTO dto)
        {
            var result = await _bookingService.UpdateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _bookingService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }

    }
}
