using System.ComponentModel.DataAnnotations;

namespace sistem_pengelolaan_lab.DTOs.Ruangan
{
    public class RuanganCreateDto
    {
        [Required(ErrorMessage = "Nama Ruangan wajib diisi.")]
        [MaxLength(255)]
        public string Nama_Ruangan { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kondisi Ruangan wajib diisi.")]
        [MaxLength(255)]
        public string Kondisi_Ruangan { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Status { get; set; } // Nullable

        // Untuk relasi Storage
        [Required(ErrorMessage = "Informasi Storage wajib diisi.")]
        public StorageCreateDto Storage { get; set; } = new StorageCreateDto();
    }

    public class StorageCreateDto
    {
        [Required(ErrorMessage = "Nama Storage wajib diisi.")]
        [MaxLength(255)]
        public string Nama_Storage { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status Storage wajib diisi.")]
        [MaxLength(100)]
        public string Status_Storage { get; set; } = string.Empty;

        [Required(ErrorMessage = "Jumlah Storage wajib diisi.")]
        [Range(1, int.MaxValue, ErrorMessage = "Jumlah Storage harus lebih dari 0.")]
        public int Jumlah_Storage { get; set; }
    }
}