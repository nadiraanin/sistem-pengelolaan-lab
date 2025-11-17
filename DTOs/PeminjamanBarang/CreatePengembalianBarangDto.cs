public class CreatePengembalianBarangDto
{
    public int IdPeminjamanBarang { get; set; }
    public DateTime TanggalKembali { get; set; }
    public string KondisiBarang { get; set; }
    public int JumlahKembali { get; set; }
    public string? CatatanPengembalian { get; set; }
}