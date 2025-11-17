public class StokOpnameReadDto
{
    public int IdStokOpname { get; set; }
    public string NamaBarang { get; set; }
    public DateTime Tanggal { get; set; }
    public int Jumlah { get; set; }
    public string Kondisi { get; set; }
    public string LokasiBarang { get; set; }
    public string Keterangan { get; set; } // Jadwal_StokOpame
    public string? FotoBukti { get; set; }
}