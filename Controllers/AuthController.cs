using sistem_pengelolaan_lab.DTOs.Auth;
using sistem_pengelolaan_lab.Helpers;
using sistem_pengelolaan_lab.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace sistem_pengelolaan_lab.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService auth) : ControllerBase
    {
        private readonly IAuthService _auth = auth;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sanitizedDto = SanitizerHelper.EncodeObject(dto);
            if (sanitizedDto == null) return NotFound();

            var res = await _auth.AuthenticateAsync(sanitizedDto);
            if (res?.Token == null) return Unauthorized(new { message = res?.ErrorMessage });
            if (!string.IsNullOrEmpty(res?.ErrorMessage)) return BadRequest(new { message = res?.ErrorMessage });
            return Ok(res);
        }

        [Authorize]
        [HttpPost("getpermission")]
        public async Task<IActionResult> GetPermission([FromBody] PermissionRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sanitizedDto = SanitizerHelper.EncodeObject(dto);
            if (sanitizedDto == null) return NotFound();

            var res = await _auth.GetPermissionAsync(sanitizedDto);
            if (res?.Token == null) return Unauthorized(new { message = res?.ErrorMessage });
            if (!string.IsNullOrEmpty(res?.ErrorMessage)) return BadRequest(new { message = res?.ErrorMessage });
            return Ok(res);
        }

        [Authorize]
        [HttpPost("getmenu")]
        public async Task<IActionResult> GetMenu([FromBody] PermissionRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var username = User.FindFirstValue("namaakun");
            var appId = User.FindFirstValue("idapp");
            var roleId = User.FindFirstValue("idrole");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(roleId))
            {
                return Unauthorized();
            }

            var sanitizedDto = SanitizerHelper.EncodeObject(dto);
            if (sanitizedDto == null) return NotFound();

            var res = await _auth.GetMenuAsync(sanitizedDto);
            if (!string.IsNullOrEmpty(res?.ErrorMessage)) return BadRequest(new { message = res?.ErrorMessage });
            return Ok(res);
        }
    }
}
