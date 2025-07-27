using System.Text.Json;
using TCSTest.Models;
using TCSTest.Repositories;
using TCSTest.Services;

namespace TCSTest.Services
{
    public class ChannelService : IChannelService
    {
        private readonly IRepository<Channel> _repository;

        public ChannelService()
        {
            _repository = new JsonRepository<Channel>("Data/channels.json");
        }

        public async Task<List<Channel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Channel?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Channel channel)
        {
            await _repository.AddAsync(channel);
        }

        public async Task UpdateAsync(Channel channel)
        {
            await _repository.UpdateAsync(channel);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
