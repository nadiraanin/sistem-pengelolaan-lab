using sistem_pengelolaan_lab.Models;

namespace sistem_pengelolaan_lab.Repositories.Interfaces
{
    public interface IStorageRepository
    {
        Task<Storage?> GetStorageByIdAsync(int id);
        Task AddStorageAsync(Storage storage);
        Task UpdateStorageAsync(Storage storage);
        Task DeleteStorageAsync(int id);
    }
}