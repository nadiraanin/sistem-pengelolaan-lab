public class CreatePeminjamanBarangDto
{
	public int IdBarang { get; set; }
	public int IdPengguna { get; set; }
	public DateTime TanggalPinjam { get; set; }
	public DateTime TanggalKembali { get; set; }
	public string AlasanPeminjaman { get; set; }
	public string? FileSurat { get; set; } // URL atau path
}