namespace sistem_pengelolaan_lab.DTOs.Ruangan
{
    public class RuanganReadDto
    {
        public int ID_Ruangan { get; set; }
        public string Nama_Ruangan { get; set; } = string.Empty;
        public string Kondisi_Ruangan { get; set; } = string.Empty;
        public string? Status { get; set; } // Nullable
        public StorageReadDto? Storage { get; set; } // Nullable, karena Ruangan bisa tanpa Storage
    }

    public class StorageReadDto
    {
        public int ID_Storage { get; set; }
        public string Nama_Storage { get; set; } = string.Empty;
        public string Status_Storage { get; set; } = string.Empty;
        public int Jumlah_Storage { get; set; }
    }
}