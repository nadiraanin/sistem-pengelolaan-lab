using System.ComponentModel.DataAnnotations;

namespace sistem_pengelolaan_lab.DTOs.Ruangan
{
    public class RuanganUpdateDto
    {
        [Required(ErrorMessage = "ID Ruangan wajib diisi.")]
        public int ID_Ruangan { get; set; }

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
        public StorageUpdateDto Storage { get; set; } = new StorageUpdateDto();
    }

    public class StorageUpdateDto
    {
        // ID_Storage diperlukan jika ingin mengupdate storage yang sudah ada
        // Jika Storage selalu dibuat baru bersama Ruangan, ID_Storage bisa diabaikan
        public int? ID_Storage { get; set; } // Bisa null jika Storage baru

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