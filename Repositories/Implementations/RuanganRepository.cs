using Microsoft.EntityFrameworkCore;
using sistem_pengelolaan_lab.Models;
using sistem_pengelolaan_lab.Repositories.Interfaces;

namespace sistem_pengelolaan_lab.Repositories.Implementations
{
    public class RuanganRepository : IRuanganRepository
    {
        private readonly ApplicationDbContext _context;

        public RuanganRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ruangan>> GetAllRuanganAsync()
        {
            return await _context.Ruangan
                                 .Include(r => r.Storages) // Load relasi Storage
                                 .Where(r => !r.IsDeleted) // Hanya yang tidak terhapus
                                 .ToListAsync();
        }

        public async Task<Ruangan?> GetRuanganByIdAsync(int id)
        {
            return await _context.Ruangan
                                 .Include(r => r.Storages) // Load relasi Storage
                                 .FirstOrDefaultAsync(r => r.ID_Ruangan == id && !r.IsDeleted);
        }

        public async Task AddRuanganAsync(Ruangan ruangan)
        {
            _context.Ruangan.Add(ruangan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRuanganAsync(Ruangan ruangan)
        {
            _context.Entry(ruangan).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRuanganAsync(int id)
        {
            var ruangan = await _context.Ruangan.FindAsync(id);
            if (ruangan != null)
            {
                ruangan.IsDeleted = true; // Logical delete
                _context.Entry(ruangan).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> RuanganExistsAsync(int id)
        {
            return await _context.Ruangan.AnyAsync(r => r.ID_Ruangan == id && !r.IsDeleted);
        }
    }
}