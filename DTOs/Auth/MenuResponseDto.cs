using sistem_pengelolaan_lab.Models;

namespace sistem_pengelolaan_lab.DTOs.Auth
{
    public class MenuResponseDto
    {
        public List<Menu> ListMenu { get; set; } = [];
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
