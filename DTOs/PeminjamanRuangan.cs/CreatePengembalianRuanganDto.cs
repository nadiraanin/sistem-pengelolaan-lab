public class CreatePengembalianRuanganDto
{
    public int IdPeminjamanRuangan { get; set; }
    public string KondisiRuangan { get; set; }
    public string? DokumentasiSebelum { get; set; } // URL
    public string? DokumentasiSesudah { get; set; } // URL
    public string? CatatanPengembalian { get; set; }
}