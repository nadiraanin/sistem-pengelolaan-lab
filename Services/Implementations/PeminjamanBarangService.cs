using Microsoft.Data.SqlClient;
using System.Data;
using Xceed.Words.NET; // Jangan lupa using directive ini

public class PeminjamanBarangService : IPeminjamanBarangService
{
    private readonly string _connectionString;

    public PeminjamanBarangService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    // TAHAP 1: Mengajukan peminjaman & men-generate surat (jika perlu)
    public async Task<(int PeminjamanId, byte[]? WordDocument)> AjukanPeminjamanAsync(CreatePeminjamanDto dto)
    {
        int peminjamanId;
        byte[]? wordFileBytes = null;

        // 1. Panggil SP untuk menyimpan pengajuan
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("dbo.usp_AjukanPeminjamanBarang", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@ID_Barang", dto.IdBarang);
            command.Parameters.AddWithValue("@ID_Pengguna", dto.IdPengguna);
            command.Parameters.AddWithValue("@Tanggal_Pinjam", dto.TanggalPinjam);
            command.Parameters.AddWithValue("@Tanggal_Kembali", dto.TanggalKembali);
            command.Parameters.AddWithValue("@Alasan", dto.Alasan);
            command.Parameters.AddWithValue("@JumlahPinjam", dto.JumlahPinjam);
            var newIdParam = new SqlParameter("@NewPeminjamanID", SqlDbType.Int) { Direction = ParameterDirection.Output };
            command.Parameters.Add(newIdParam);
            await command.ExecuteNonQueryAsync();
            peminjamanId = Convert.ToInt32(newIdParam.Value);
        }

        // 2. Cek apakah barangnya 'Asset' untuk generate surat
        var barangDetail = await GetDetailBarangUntukSuratAsync(dto.IdBarang);
        if (barangDetail.JenisBarang == "Asset")
        {
            var penggunaDetail = await GetDetailPenggunaUntukSuratAsync(dto.IdPengguna);
            wordFileBytes = GenerateWordDocument(barangDetail, penggunaDetail, dto);
        }

        return (peminjamanId, wordFileBytes);
    }

    // TAHAP 2: Menerima file upload dan menyimpannya ke database
    public async Task ProsesUploadSuratAsync(int idPeminjaman, IFormFile file)
    {
        byte[] fileBytes;
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            fileBytes = memoryStream.ToArray();
        }

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("dbo.usp_UploadSuratVarbinary", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@ID_PeminjamanBarang", idPeminjaman);
            command.Parameters.AddWithValue("@FileBinary", fileBytes);
            await command.ExecuteNonQueryAsync();
        }
    }

    // === METODE HELPER (perlu Anda lengkapi query-nya) ===

    private byte[] GenerateWordDocument(BarangSuratDto barang, PenggunaSuratDto pengguna, CreatePeminjamanDto peminjaman)
    {
        string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "template_peminjaman.docx");
        using (var memoryStream = new MemoryStream())
        {
            using (var document = DocX.Load(templatePath))
            {
                document.ReplaceText("{{NAMA_PEMINJAM}}", pengguna.NamaPengguna);
                document.ReplaceText("{{NAMA_BARANG}}", barang.NamaBarang);
                document.ReplaceText("{{SERIAL_NUMBER}}", barang.SerialNumber ?? "-");
                document.ReplaceText("{{TANGGAL_PINJAM}}", peminjaman.TanggalPinjam.ToString("dd MMMM yyyy"));
                document.SaveAs(memoryStream);
            }
            return memoryStream.ToArray();
        }
    }

    private async Task<BarangSuratDto> GetDetailBarangUntukSuratAsync(int idBarang)
    {
        // LENGKAPI INI: Query sederhana untuk mengambil Nama_Barang, Jenis_Barang, Serial_Number dari tabel Barang
        // Contoh: "SELECT Nama_Barang, Jenis_Barang, Serial_Number FROM Barang WHERE ID_Barang = @idBarang"
    }
    private async Task<PenggunaSuratDto> GetDetailPenggunaUntukSuratAsync(int idPengguna)
    {
        // LENGKAPI INI: Query sederhana untuk mengambil Nama_Pengguna dari tabel Pengguna
        // Contoh: "SELECT Nama_Pengguna FROM Pengguna WHERE ID_Pengguna = @idPengguna"
    }
}