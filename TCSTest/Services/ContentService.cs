using System.Text.Json;
using TCSTest.Models;
using TCSTest.Repositories;

namespace TCSTest.Services
{
    public class ContentService : IContentService
    {
        private readonly IRepository<Content> _repository;

        public ContentService()
        {
            _repository = new JsonRepository<Content>("Data/content_catalog.json");
        }

        public async Task<List<Content>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Content?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Content content)
        {
            await _repository.AddAsync(content);
        }

        public async Task UpdateAsync(Content content)
        {
            await _repository.UpdateAsync(content);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
