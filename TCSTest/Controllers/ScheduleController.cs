using Microsoft.AspNetCore.Mvc;
using TCSTest.Services;
using System;
using TCSTest.Models;

namespace TCSTest.Controllers
{
    [ApiController]
    [Route("api/schedule")]
    public class ScheduleController : ControllerBase
    {
        private readonly IChannelScheduleService _service;

        public ScheduleController(IChannelScheduleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAll()
        {
            try
            {
                var schedules = await _service.GetAllAsync();
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("channel/{channelId}")]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetByChannelId(Guid channelId)
        {
            try
            {
                var schedules = await _service.GetAllAsync();
                var channelSchedules = schedules.Where(s => s.ChannelId == channelId).ToList();
                if (!channelSchedules.Any()) return NotFound();
                return Ok(channelSchedules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetById(Guid id)
        {
            try
            {
                var channelSchedule = await _service.GetByIdAsync(id);
                if (channelSchedule is null) return NotFound();
                return Ok(channelSchedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Schedule schedule)
        {
            try
            {
                await _service.AddAsync(schedule);
                return CreatedAtAction(nameof(GetById), new { id = schedule.Id }, schedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{channelId}/{contentId}")]
        public async Task<IActionResult> Update(Guid channelId, Guid contentId, Schedule schedule)
        {
            try
            {
                if (channelId != schedule.ChannelId || contentId != schedule.ContentId)
                    return BadRequest("ChannelId or ContentId does not match the schedule.");

                var schedules = await _service.GetAllAsync();
                var existing = schedules.FirstOrDefault(s => s.ChannelId == channelId && s.ContentId == contentId);

                if (existing == null)
                    return NotFound();

                schedule.Id = existing.Id; // Preserve the original Id
                await _service.UpdateAsync(schedule);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{channelId}/{contentId}")]
        public async Task<IActionResult> Delete(Guid channelId, Guid contentId)
        {
            try
            {
                var schedules = await _service.GetAllAsync();
                var existing = schedules.FirstOrDefault(s => s.ChannelId == channelId && s.ContentId == contentId);
                if (existing == null)
                    return NotFound();
                await _service.DeleteAsync(existing.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("now")]
        public async Task<ActionResult<IEnumerable<ScheduleDetailsDto>>> GetCurrentlyAiring([FromQuery] Guid? channelId, [FromQuery] DateTime? date)
        {
            try
            {
                var schedules = await _service.GetCurrentlyAiringAsync(channelId, date);
                var channels = await new ChannelService().GetAllAsync();
                var contents = await new ContentService().GetAllAsync();

                var result = schedules.Select(s => new ScheduleDetailsDto
                {
                    Id = s.Id,
                    ChannelId = s.ChannelId,
                    ChannelTitle = channels.FirstOrDefault(c => c.Id == s.ChannelId)?.Name ?? "",
                    ContentId = s.ContentId,
                    ContentTitle = contents.FirstOrDefault(c => c.Id == s.ContentId)?.Title ?? "",
                    StartTime = s.StartTime,
                    EndTime = s.EndTime
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
