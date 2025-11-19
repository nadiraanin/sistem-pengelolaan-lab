public class CreatePeminjamanBarangDto
{
    public int IdBarang { get; set; }
    public int IdPengguna { get; set; }
    public DateTime TanggalPinjam { get; set; }
    public DateTime TanggalKembali { get; set; }
    public string Alasan { get; set; }
    public int JumlahPinjam { get; set; }
}
public class BarangSuratDto { public string NamaBarang { get; set; } public string JenisBarang { get; set; } public string? SerialNumber { get; set; } }
public class PenggunaSuratDto { public string NamaPengguna { get; set; } }