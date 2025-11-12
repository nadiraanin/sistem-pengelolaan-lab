using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sistem_pengelolaan_lab.DTOs.Ruangan;
using sistem_pengelolaan_lab.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sistem_pengelolaan_lab.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] // Tambahkan jika semua endpoint memerlukan otentikasi
    public class RuanganController : ControllerBase
    {
        private readonly IRuanganService _ruanganService;

        public RuanganController(IRuanganService ruanganService)
        {
            _ruanganService = ruanganService;
        }

        // GET: api/Ruangan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuanganReadDto>>> GetRuangan()
        {
            var ruangans = await _ruanganService.GetAllRuanganAsync();
            return Ok(ruangans);
        }

        // GET: api/Ruangan/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RuanganReadDto>> GetRuanganById(int id)
        {
            var ruangan = await _ruanganService.GetRuanganByIdAsync(id);
            if (ruangan == null)
            {
                return NotFound("Ruangan tidak ditemukan.");
            }
            return Ok(ruangan);
        }

        // POST: api/Ruangan
        [HttpPost]
        // [Authorize(Policy = "HasPermission")] // Contoh kebijakan otorisasi
        public async Task<ActionResult<RuanganReadDto>> PostRuangan(RuanganCreateDto ruanganDto)
        {
            var createdRuangan = await _ruanganService.CreateRuanganAsync(ruanganDto);
            return CreatedAtAction(nameof(GetRuanganById), new { id = createdRuangan.ID_Ruangan }, createdRuangan);
        }

        // PUT: api/Ruangan/5
        [HttpPut("{id}")]
        // [Authorize(Policy = "HasPermission")]
        public async Task<IActionResult> PutRuangan(int id, RuanganUpdateDto ruanganDto)
        {
            if (id != ruanganDto.ID_Ruangan)
            {
                return BadRequest("ID Ruangan di URL tidak cocok dengan ID di body.");
            }

            var success = await _ruanganService.UpdateRuanganAsync(ruanganDto);
            if (!success)
            {
                return NotFound("Ruangan tidak ditemukan.");
            }
            return NoContent(); // 204 No Content for successful update
        }

        // DELETE: api/Ruangan/5 (Logical Delete)
        [HttpDelete("{id}")]
        // [Authorize(Policy = "HasPermission")]
        public async Task<IActionResult> DeleteRuangan(int id)
        {
            var success = await _ruanganService.DeleteRuanganAsync(id);
            if (!success)
            {
                return NotFound("Ruangan tidak ditemukan.");
            }
            return NoContent(); // 204 No Content for successful delete
        }
    }
}