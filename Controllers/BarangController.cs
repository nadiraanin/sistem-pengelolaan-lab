// File: BarangController.cs

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/barang")]
public class BarangController : ControllerBase
{
	private readonly IBarangService _barangService;

	// Gunakan Dependency Injection untuk menyuntikkan service Anda
	public BarangController(IBarangService barangService)
	{
		_barangService = barangService;
	}

	[HttpPost] // Endpoint ini merespons HTTP POST ke /api/barang
	public async Task<IActionResult> AddBarang([FromBody] CreateBarangDto barangDto)
	{
		try
		{
			// Panggil service untuk menjalankan logika database
			int newBarangId = await _barangService.AddBarangAsync(barangDto);

			// Jika berhasil, kirim response HTTP 200 OK dengan ID barang yang baru
			return Ok(new
			{
				Message = "Barang berhasil ditambahkan!",
				IdBarangBaru = newBarangId
			});
		}
		catch (Exception ex)
		{
			// Jika Stored Procedure melempar error (THROW), tangkap pesannya
			// dan kirim sebagai response HTTP 400 Bad Request
			return BadRequest(new { ErrorMessage = ex.Message });
		}
	}
}