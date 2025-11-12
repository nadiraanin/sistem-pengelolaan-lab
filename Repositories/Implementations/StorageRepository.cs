using Microsoft.EntityFrameworkCore;
using sistem_pengelolaan_lab.Models;
using sistem_pengelolaan_lab.Repositories.Interfaces;
using System.Threading.Tasks; // Pastikan ini ada

namespace sistem_pengelolaan_lab.Repositories.Implementations
{
    public class StorageRepository : IStorageRepository
    {
        private readonly ApplicationDbContext _context;

        public StorageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Implementasi GetStorageByIdAsync harus cocok dengan interface
        public async Task<Storage?> GetStorageByIdAsync(int id)
        {
            return await _context.Storage.FindAsync(id);
        }

        public async Task AddStorageAsync(Storage storage) // Implementasi AddStorageAsync
        {
            _context.Storage.Add(storage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStorageAsync(Storage storage) // Implementasi UpdateStorageAsync
        {
            _context.Entry(storage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStorageAsync(int id) // Implementasi DeleteStorageAsync
        {
            var storage = await _context.Storage.FindAsync(id);
            if (storage != null)
            {
                _context.Storage.Remove(storage);
                await _context.SaveChangesAsync();
            }
        }
    }
}