using System.Text.Json;
using TCSTest.Models;
using TCSTest.Repositories;
using TCSTest.Services;

namespace TCSTest.Services
{
    public class ChannelScheduleService : IChannelScheduleService
    {
        private readonly IRepository<Schedule> _repository;

        public ChannelScheduleService()
        {
            _repository = new JsonRepository<Schedule>("Data/channel_schedule.json");
        }

        public async Task<List<Schedule>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Schedule?> GetByIdAsync(Guid id)
        {
            var schedules = await GetAllAsync();
            return schedules.FirstOrDefault(s => s.Id == id);
        }

        public async Task AddAsync(Schedule schedule)
        {
            await _repository.AddAsync(schedule);
        }

        public async Task UpdateAsync(Schedule schedule)
        {
            await _repository.UpdateAsync(schedule);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<Schedule>> GetCurrentlyAiringAsync(Guid? channelId = null, DateTime? date = null)
        {
            var targetDate = (date ?? DateTime.Now).Date;
            var schedules = await GetAllAsync();
            return schedules.Where(s =>
                (channelId == null || s.ChannelId == channelId) &&
                s.StartTime.Date <= targetDate && s.EndTime.Date >= targetDate
            ).ToList();
        }
    }
}
