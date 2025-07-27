using TCSTest.Models;

namespace TCSTest.Services
{
    public interface IChannelService
    {
        Task<List<Channel>> GetAllAsync();
        Task<Channel?> GetByIdAsync(Guid id);
        Task AddAsync(Channel channel);
        Task UpdateAsync(Channel channel);
        Task DeleteAsync(Guid id);
    }
}