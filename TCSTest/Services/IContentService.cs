using TCSTest.Models;

namespace TCSTest.Services
{
    public interface IContentService
    {
        Task<List<Content>> GetAllAsync();
        Task<Content?> GetByIdAsync(Guid id);
        Task AddAsync(Content content);
        Task UpdateAsync(Content content);
        Task DeleteAsync(Guid id);
    }
}