namespace astratech_apps_backend.Services.Interfaces
{
    public interface ILDAPService
    {
        Task<(bool IsSuccess, string? ErrorMessage)> AuthenticateAsync(string username, string password);
        Task<string?> GetUsernameAsync(string samAccountName);
        Task<string?> GetMailAsync(string samAccountName);
        Task<string?> GetDisplayNameAsync(string samAccountName);
    }
}
