using AutoMapper;                                   // Untuk IMapper
using sistem_pengelolaan_lab.DTOs.Ruangan;       // Untuk RuanganCreateDto & RuanganUpdateDto
using sistem_pengelolaan_lab.Repositories.Interfaces; // Untuk IStorageRepository
using sistem_pengelolaan_lab.Services.Interfaces;    // Untuk IRuanganService
using sistem_pengelolaan_lab.Models;

namespace sistem_pengelolaan_lab.Services.Implementations
{
    public class RuanganService : IRuanganService
    {
        private readonly IRuanganRepository _ruanganRepository;
        private readonly IStorageRepository _storageRepository; // Untuk manajemen Storage
        private readonly IMapper _mapper;
        private readonly string _connectionString;

        public RuanganService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Implementasi metode untuk dropdown
        public async Task<IEnumerable<RuanganReadDto>> GetRuanganForDropdownAsync()
        {
            var list = new List<RuanganReadDto>();
            using (var connection = new SqlConnection(_connectionString))
            {
                // Panggil SP yang sudah kita buat
                var command = new SqlCommand("dbo.usp_GetRuanganForDropdown", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new RuanganReadDto
                        {
                            IdRuangan = reader.GetInt32(reader.GetOrdinal("ID_Ruangan")),
                            NamaRuangan = reader.GetString(reader.GetOrdinal("Nama_Ruangan"))
                            // Properti lain di DTO ini bisa null karena SP hanya mengembalikan ID & Nama
                        });
                    }
                }
            }
            return list;
        }
        public RuanganService(IRuanganRepository ruanganRepository, IStorageRepository storageRepository, IMapper mapper)
        {
            _ruanganRepository = ruanganRepository;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RuanganReadDto>> GetAllRuanganAsync()
        {
            var ruangans = await _ruanganRepository.GetAllRuanganAsync();
            return _mapper.Map<IEnumerable<RuanganReadDto>>(ruangans);
        }

        public async Task<RuanganReadDto?> GetRuanganByIdAsync(int id)
        {
            var ruangan = await _ruanganRepository.GetRuanganByIdAsync(id);
            if (ruangan == null) return null;
            return _mapper.Map<RuanganReadDto>(ruangan);
        }

        public async Task<RuanganReadDto> CreateRuanganAsync(RuanganCreateDto ruanganDto)
        {
            var ruangan = _mapper.Map<Ruangan>(ruanganDto);
            ruangan.IsDeleted = false; // Default untuk Create

            // Buat instance Storage dari DTO
            var storage = _mapper.Map<Storage>(ruanganDto.Storage);

            // Tambahkan Storage ke Ruangan (EF Core akan menangani relasi)
            ruangan.Storages = new List<Storage> { storage };

            await _ruanganRepository.AddRuanganAsync(ruangan); // Ini akan menyimpan Ruangan dan Storage

            // Perbarui ID_Ruangan di Storage setelah Ruangan tersimpan dan mendapatkan ID
            storage.ID_Ruangan = ruangan.ID_Ruangan;
            await _storageRepository.UpdateStorageAsync(storage); // Simpan kembali storage untuk FK

            return _mapper.Map<RuanganReadDto>(ruangan);
        }

        public async Task<bool> UpdateRuanganAsync(RuanganUpdateDto ruanganDto)
        {
            var existingRuangan = await _ruanganRepository.GetRuanganByIdAsync(ruanganDto.ID_Ruangan);
            if (existingRuangan == null) return false;

            // Update properti Ruangan
            _mapper.Map(ruanganDto, existingRuangan);

            // Update Storage yang terkait
            var existingStorage = existingRuangan.Storages?.FirstOrDefault();
            if (existingStorage != null)
            {
                _mapper.Map(ruanganDto.Storage, existingStorage);
                await _storageRepository.UpdateStorageAsync(existingStorage);
            }
            else // Jika Ruangan sebelumnya tidak punya Storage dan sekarang ingin ditambahkan
            {
                var newStorage = _mapper.Map<Storage>(ruanganDto.Storage);
                newStorage.ID_Ruangan = existingRuangan.ID_Ruangan;
                await _storageRepository.AddStorageAsync(newStorage);
            }

            await _ruanganRepository.UpdateRuanganAsync(existingRuangan);
            return true;
        }

        public async Task<bool> DeleteRuanganAsync(int id)
        {
            if (!await _ruanganRepository.RuanganExistsAsync(id))
            {
                return false;
            }
            await _ruanganRepository.DeleteRuanganAsync(id); // Logical delete
            return true;
        }
    }
}