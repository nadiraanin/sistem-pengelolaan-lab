using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sistem_pengelolaan_lab.Models;

namespace sistem_pengelolaan_lab.Models
{
    [Table("Storage")]
    public class Storage
    {
        [Key]
        [Column("ID_Storage")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tambahkan ini jika ID_Storage auto-increment
        public int ID_Storage { get; set; }

        [Column("ID_Ruangan")]
        public int ID_Ruangan { get; set; }

        [Column("Nama_Storage")]
        [MaxLength(255)]
        public string Nama_Storage { get; set; } = string.Empty; // Beri nilai default

        [Column("Status_Storage")]
        [MaxLength(100)]
        public string Status_Storage { get; set; } = string.Empty; // Beri nilai default

        [Column("jumlah_storage")]
        public int Jumlah_Storage { get; set; } // int tidak perlu default, defaultnya 0

        [ForeignKey("ID_Ruangan")]
        public virtual Ruangan Ruangan { get; set; }
    }
}