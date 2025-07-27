using TCSTest.Models;

namespace TCSTest.Services
{
    public interface IChannelScheduleService
    {
        Task<List<Schedule>> GetAllAsync();
        Task<Schedule?> GetByIdAsync(Guid id);
        Task AddAsync(Schedule schedule);
        Task UpdateAsync(Schedule schedule);
        Task DeleteAsync(Guid id);
        Task<List<Schedule>> GetCurrentlyAiringAsync(Guid? channelId = null, DateTime? date = null);
    }
}