public class BarangReadDto
{
    public int IdBarang { get; set; }
    public string NamaBarang { get; set; }
    public int StokBarang { get; set; }
    public string StatusBarang { get; set; }
    public string JenisBarang { get; set; }
    public string? SerialNumber { get; set; }
    public string KondisiBarang { get; set; }
    public string NamaStorage { get; set; }
    public string NamaRuangan { get; set; }
    public string DaftarPerlengkapan { get; set; } // Hasil dari STRING_AGG
}