// Nama file baru: Controllers/PeminjamanBarangController.cs
[ApiController]
[Route("api/peminjaman-barang")] // *** ROUTE DIUBAH ***
public class PeminjamanBarangController : ControllerBase
{
    private readonly IPeminjamanBarangService _peminjamanBarangService; // Tipe diubah

    public PeminjamanBarangController(IPeminjamanBarangService peminjamanBarangService) // Tipe diubah
    {
        _peminjamanBarangService = peminjamanBarangService;
    }

    // Endpoint menjadi: POST /api/peminjaman-barang/ajukan
    [HttpPost("ajukan")]
    public async Task<IActionResult> AjukanPeminjaman([FromBody] CreatePeminjamanBarangDto dto) // Tipe diubah
    {
        try
        {
            var (peminjamanId, wordDocument) = await _peminjamanBarangService.AjukanPeminjamanAsync(dto); // Metode dipanggil dari service baru

            if (wordDocument != null)
            {
                return File(wordDocument, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"Surat_Peminjaman_{peminjamanId}.docx");
            }
            else
            {
                return Ok(new { Message = "Peminjaman non-asset berhasil diajukan.", IdPeminjaman = peminjamanId });
            }
        }
        catch (Exception ex) { return BadRequest(new { ErrorMessage = ex.Message }); }
    }

    // Endpoint menjadi: POST /api/peminjaman-barang/{idPeminjaman}/upload-surat
    [HttpPost("{idPeminjaman}/upload-surat")]
    public async Task<IActionResult> UploadSurat(int idPeminjaman, IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("File tidak boleh kosong.");
        try
        {
            await _peminjamanBarangService.ProsesUploadSuratAsync(idPeminjaman, file); // Metode dipanggil dari service baru
            return Ok(new { Message = "Surat persetujuan berhasil di-upload." });
        }
        catch (Exception ex) { return BadRequest(new { ErrorMessage = ex.Message }); }
    }
}