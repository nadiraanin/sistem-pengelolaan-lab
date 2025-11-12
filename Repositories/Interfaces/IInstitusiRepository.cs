using sistem_pengelolaan_lab.DTOs.Institusi;
using sistem_pengelolaan_lab.Models;

namespace sistem_pengelolaan_lab.Repositories.Interfaces
{
    public interface IInstitusiRepository
    {
        Task<(IEnumerable<Institusi>, int totalData)> GetAllAsync(GetAllInstitusiRequest dto);
        Task<Institusi?> GetByIdAsync(short id);
        Task<int> CreateAsync(CreateInstitusiRequest dto, string createdBy);
        Task<bool> UpdateAsync(UpdateInstitusiRequest dto, string updatedBy);
        Task<bool> SetStatusAsync(short id, string updatedBy);
    }
}
