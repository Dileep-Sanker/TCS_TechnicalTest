using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using TCSTest.Repositories;

namespace TCSTest.Repositories
{
    public class JsonRepository<T> : IRepository<T> where T : class
    {
        private readonly string _filePath;

        public JsonRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<List<T>> GetAllAsync()
        {
            if (!File.Exists(_filePath)) return new List<T>();
            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var items = await GetAllAsync();
            var prop = typeof(T).GetProperty("Id");
            return items.FirstOrDefault(x => prop != null && prop.GetValue(x)?.Equals(id) == true);
        }

        public async Task AddAsync(T entity)
        {
            var items = await GetAllAsync();
            var prop = typeof(T).GetProperty("Id");
            if (prop != null && prop.PropertyType == typeof(Guid))
                prop.SetValue(entity, Guid.NewGuid());
            items.Add(entity);
            await SaveAsync(items);
        }

        public async Task UpdateAsync(T entity)
        {
            var items = await GetAllAsync();
            var prop = typeof(T).GetProperty("Id");
            if (prop == null) return;
            var id = prop.GetValue(entity);
            var idx = items.FindIndex(x => prop.GetValue(x)?.Equals(id) == true);
            if (idx >= 0)
            {
                items[idx] = entity;
                await SaveAsync(items);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var items = await GetAllAsync();
            var prop = typeof(T).GetProperty("Id");
            items.RemoveAll(x => prop != null && prop.GetValue(x)?.Equals(id) == true);
            await SaveAsync(items);
        }

        private async Task SaveAsync(List<T> items)
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}