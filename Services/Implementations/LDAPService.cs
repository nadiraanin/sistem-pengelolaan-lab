using astratech_apps_backend.Services.Interfaces;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Text;

namespace astratech_apps_backend.Services.Implementations
{
    public class LDAPService(IConfiguration configuration, ILogger<LDAPService> logger) : ILDAPService
    {
        private readonly string _ldapServer = configuration["Key:LDAPServer"]!;
        private readonly string _ldapDN = configuration["Key:LDAPDN"]!;
        private readonly ILogger<LDAPService> _logger = logger;

        public async Task<(bool IsSuccess, string? ErrorMessage)> AuthenticateAsync(string username, string password)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using var connection = new LdapConnection(_ldapServer);
                    connection.AuthType = AuthType.Basic;
                    connection.SessionOptions.ReferralChasing = ReferralChasingOptions.None;
                    connection.Bind(new NetworkCredential($"polman\\{username}", password));

                    return (true, "");
                }
                catch (LdapException ex)
                {
                    _logger.LogError(ex, "Gagal melakukan autentikasi untuk username {Username}. Error code: {Code}", username, ex.ErrorCode);

                    if (ex.ErrorCode == 49) return (false, "Username atau password tidak valid.");
                    return (false, $"Koneksi ke server LDAP gagal: {ex.Message}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Terjadi kesalahan pada proses autentikasi.");
                    return (false, "Terjadi kesalahan pada proses autentikasi.");
                }
            });
        }

        private async Task<string?> GetAttributeAsync(string samAccountName, string attributeName)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using var connection = new LdapConnection(_ldapServer);
                    connection.AuthType = AuthType.Basic;
                    connection.SessionOptions.ReferralChasing = ReferralChasingOptions.None;
                    connection.Bind(new NetworkCredential($"polman\\{configuration["Key:LDAPSSOManagerUsername"]!}", configuration["Key:LDAPSSOManagerPassword"]!));

                    string filter = $"(sAMAccountName={samAccountName})";

                    var searchRequest = new SearchRequest(
                        _ldapDN,
                        filter,
                        SearchScope.Subtree,
                        attributeName
                    )
                    {
                        SizeLimit = 1
                    };

                    var searchResponse = (SearchResponse)connection.SendRequest(searchRequest);

                    if (searchResponse.Entries.Count > 0)
                    {
                        var entry = searchResponse.Entries[0];
                        if (entry.Attributes.Contains(attributeName))
                        {
                            var attribute = entry.Attributes[attributeName];
                            return Encoding.UTF8.GetString((byte[])attribute.GetValues(typeof(byte[]))[0]);
                        }
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Gagal mendapatkan atribut {Attribute} untuk username {User}", attributeName, samAccountName);
                    return ex.Message;
                }
            });
        }

        public Task<string?> GetUsernameAsync(string samAccountName)
        {
            return GetAttributeAsync(samAccountName, "sAMAccountName");
        }

        public Task<string?> GetMailAsync(string samAccountName)
        {
            return GetAttributeAsync(samAccountName, "mail");
        }

        public Task<string?> GetDisplayNameAsync(string samAccountName)
        {
            return GetAttributeAsync(samAccountName, "displayName");
        }
    }
}
