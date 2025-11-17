public class LaporanPeminjamanRuanganDto
{
    public int IdPeminjamanRuangan { get; set; }
    public string NamaPengguna { get; set; }
    public string NamaRuangan { get; set; }
    public DateTime TanggalMulai { get; set; }
    public DateTime TanggalSelesai { get; set; }
    public string Waktu { get; set; } // Gabungan WaktuMulai - WaktuSelesai
    public string StatusPeminjaman { get; set; }
    public string NamaPIC { get; set; }
}