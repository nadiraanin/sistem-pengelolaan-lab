public class LaporanPeminjamanBarangDto
{
    public int IdPeminjamanBarang { get; set; }
    public string NamaPengguna { get; set; }
    public string NamaBarang { get; set; }
    public string JenisBarang { get; set; }
    public string? SerialNumber { get; set; }
    public DateTime TanggalPinjam { get; set; }
    public DateTime TanggalRencanaKembali { get; set; }
    public string StatusPeminjaman { get; set; }
    public DateTime? TanggalPengembalianAktual { get; set; }
    public string? KondisiSaatKembali { get; set; }
}