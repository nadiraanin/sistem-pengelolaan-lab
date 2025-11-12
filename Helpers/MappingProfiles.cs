using AutoMapper;
using sistem_pengelolaan_lab.Models;
using sistem_pengelolaan_lab.DTOs.Ruangan;

namespace sistem_pengelolaan_lab.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Ruangan Mappings
            CreateMap<RuanganCreateDto, Ruangan>();
            CreateMap<RuanganUpdateDto, Ruangan>();
            CreateMap<Ruangan, RuanganReadDto>()
                .ForMember(dest => dest.Storage, opt => opt.MapFrom(src => src.Storages!.FirstOrDefault())); // Mengambil storage pertama, asumsi 1 ruangan 1 storage utama

            // Storage Mappings
            CreateMap<StorageCreateDto, Storage>();
            CreateMap<StorageUpdateDto, Storage>();
            CreateMap<Storage, StorageReadDto>();
        }
    }
}