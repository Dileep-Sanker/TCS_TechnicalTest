using Microsoft.AspNetCore.Mvc;
using TCSTest.Models;
using TCSTest.Services;
using System;

namespace TCSTest.Controllers
{
    [ApiController]
    [Route("api/channels")]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelService _service;

        public ChannelController(IChannelService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Channel>>> GetAll()
        {
            try
            {
                var channels = await _service.GetAllAsync();
                return Ok(channels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Channel>> GetById(Guid id)
        {
            try
            {
                var channel = await _service.GetByIdAsync(id);
                if (channel is null) return NotFound();
                return Ok(channel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Channel channel)
        {
            try
            {
                await _service.AddAsync(channel);
                return CreatedAtAction(nameof(GetById), new { id = channel.Id }, channel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Channel channel)
        {
            try
            {
                if (id != channel.Id) return BadRequest();
                await _service.UpdateAsync(channel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
