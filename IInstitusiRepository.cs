using sistem_pengelolaan_lab.Models;
using sistem_pengelolaan_lab.DTOs.Institusi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sistem_pengelolaan_lab.Repositories.Interfaces
{
    public interface IInstitusiRepository
    {
        Task<int> CreateAsync(CreateInstitusiRequest dto, string createdBy);
        Task<(IEnumerable<Institusi>, int totalData)> GetAllAsync(GetAllInstitusiRequest dto);
        Task<Institusi?> GetByIdAsync(short id);
        Task<bool> SetStatusAsync(short id, string updatedBy);
        Task<bool> UpdateAsync(UpdateInstitusiRequest dto, string updatedBy);
    }
}