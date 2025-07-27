using Microsoft.AspNetCore.Mvc;
using TCSTest.Services;
using System;
using TCSTest.Models;

namespace TCSTest.Controllers
{
    [ApiController]
    [Route("api/content")]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _service;

        public ContentController(IContentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Content>>> GetAll()
        {
            try
            {
                var contents = await _service.GetAllAsync();
                return Ok(contents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Content>> GetById(Guid id)
        {
            try
            {
                var content = await _service.GetByIdAsync(id);
                if (content is null) return NotFound();
                return Ok(content);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Content content)
        {
            try
            {
                await _service.AddAsync(content);
                return CreatedAtAction(nameof(GetById), new { id = content.Id }, content);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Content content)
        {
            try
            {
                if (id != content.Id) return BadRequest();
                await _service.UpdateAsync(content);
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
