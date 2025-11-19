using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/barang")] // Base URL untuk semua endpoint di controller ini
public class BarangController : ControllerBase
{
    private readonly IBarangService _barangService;

    public BarangController(IBarangService barangService)
    {
        _barangService = barangService;
    }

    // Endpoint untuk CREATE
    // POST /api/barang
    [HttpPost]
    public async Task<IActionResult> AddBarang([FromBody] CreateBarangDto barangDto)
    {
        try
        {
            int newBarangId = await _barangService.AddBarangAsync(barangDto);
            return Ok(new { Message = "Barang berhasil ditambahkan!", IdBarangBaru = newBarangId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ErrorMessage = ex.Message });
        }
    }

    // Endpoint untuk READ ALL
    // GET /api/barang
    [HttpGet]
    public async Task<IActionResult> GetAllBarang()
    {
        var barangList = await _barangService.GetAllBarangAsync();
        return Ok(barangList);
    }

    // Endpoint untuk READ BY ID
    // GET /api/barang/12
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBarangById(int id)
    {
        var barang = await _barangService.GetBarangByIdAsync(id);
        if (barang == null)
        {
            return NotFound(new { Message = $"Barang dengan ID {id} tidak ditemukan." });
        }
        return Ok(barang);
    }

    // Endpoint untuk UPDATE
    // PUT /api/barang/12
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBarang(int id, [FromBody] UpdateBarangDto barangDto)
    {
        try
        {
            await _barangService.UpdateBarangAsync(id, barangDto);
            return Ok(new { Message = $"Barang dengan ID {id} berhasil diperbarui." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ErrorMessage = ex.Message });
        }
    }

    // Endpoint untuk DELETE (Soft Delete)
    // DELETE /api/barang/12
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeleteBarang(int id)
    {
        try
        {
            await _barangService.SoftDeleteBarangAsync(id);
            return Ok(new { Message = $"Barang dengan ID {id} berhasil dinonaktifkan." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ErrorMessage = ex.Message });
        }
    }
}