using System.Collections.Generic; // Untuk koleksi storage
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sistem_pengelolaan_lab.Models;

namespace sistem_pengelolaan_lab.Models
{
    public class Ruangan
    {
        [Key] // Menandakan ini adalah Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Menandakan ini auto-increment
        public int ID_Ruangan { get; set; }

        [Required] // Kolom tidak boleh null
        [StringLength(255)] // Batasan panjang string
        public string Nama_Ruangan { get; set; }

        [StringLength(255)]
        public string? Kondisi_Ruangan { get; set; } // Nullable dengan ?

        [StringLength(100)]
        public string? Status { get; set; } // Nullable

        [Required]
        public bool IsDeleted { get; set; } = false; // Default value di aplikasi

        // Navigasi properti untuk relasi One-to-Many ke Storage
        public virtual ICollection<Storage> Storages { get; set; } = new List<Storage>();
    }
}