using FMA.BLL.Services.Interfaces;
using FMA.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FMA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PitchController : ControllerBase
    {
        private readonly IPitchService _pitchService;

        public PitchController(IPitchService pitchService)
        {
            _pitchService = pitchService;
        }

        /// <summary>
        /// Get all pitches
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetPitches()
        {
            var response = await _pitchService.GetAllPitchesAsync();
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Get pitch by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPitchById(Guid id)
        {
            var response = await _pitchService.GetPitchByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Create new pitch
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreatePitch([FromBody] CreatePitchDTO createPitchDto)
        {
            var response = await _pitchService.CreatePitchAsync(createPitchDto);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Update pitch
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdatePitch([FromBody] UpdatePitchDTO updatePitchDto)
        {
            var response = await _pitchService.UpdatePitchAsync(updatePitchDto);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Delete pitch
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePitch(Guid id)
        {
            var response = await _pitchService.DeletePitchAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
} 