// Nama file baru: Services/Interfaces/IPeminjamanBarangService.cs
public interface IPeminjamanBarangService
{
	Task<(int PeminjamanId, byte[]? WordDocument)> AjukanPeminjamanAsync(CreatePeminjamanBarangDto dto);
	Task ProsesUploadSuratAsync(int idPeminjaman, IFormFile file);
}