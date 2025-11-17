public class CreatePeminjamanRuanganDto
{
    public int IdRuangan { get; set; }
    public int IdPengguna { get; set; }
    public string AlasanPeminjamanRuangan { get; set; }
    public TimeSpan WaktuMulai { get; set; } // ex: "09:00:00"
    public TimeSpan WaktuSelesai { get; set; } // ex: "17:00:00"
    public string NamaPIC { get; set; }
    public string KontakPIC { get; set; }
    public DateTime TanggalMulai { get; set; }
    public DateTime TanggalSelesai { get; set; }
}