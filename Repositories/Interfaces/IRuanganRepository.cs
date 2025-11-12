using sistem_pengelolaan_lab.Models;

namespace sistem_pengelolaan_lab.Repositories.Interfaces
{
    public interface IRuanganRepository
    {
        Task<IEnumerable<Ruangan>> GetAllRuanganAsync();
        Task<Ruangan?> GetRuanganByIdAsync(int id);
        Task AddRuanganAsync(Ruangan ruangan);
        Task UpdateRuanganAsync(Ruangan ruangan);
        Task DeleteRuanganAsync(int id); // Logical delete
        Task<bool> RuanganExistsAsync(int id);
    }
}