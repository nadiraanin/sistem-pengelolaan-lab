using sistem_pengelolaan_lab.DTOs.Ruangan;

namespace sistem_pengelolaan_lab.Services.Interfaces
{
    public interface IRuanganService
    {
        Task<IEnumerable<RuanganReadDto>> GetAllRuanganAsync();
        Task<IEnumerable<RuanganReadDto>> GetRuanganForDropdownAsync();
        Task<RuanganReadDto?> GetRuanganByIdAsync(int id);
        Task<RuanganReadDto> CreateRuanganAsync(RuanganCreateDto ruanganDto);
        Task<bool> UpdateRuanganAsync(RuanganUpdateDto ruanganDto);
        Task<bool> DeleteRuanganAsync(int id);
    }
}