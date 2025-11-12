using sistem_pengelolaan_lab.DTOs.Auth;

namespace sistem_pengelolaan_lab.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto dto);
        Task<PermissionResponseDto?> GetPermissionAsync(PermissionRequestDto dto);
        Task<MenuResponseDto?> GetMenuAsync(PermissionRequestDto dto);
    }
}
