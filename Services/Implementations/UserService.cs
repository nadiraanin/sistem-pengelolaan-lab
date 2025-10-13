using astratech_apps_backend.Models;
using astratech_apps_backend.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace astratech_apps_backend.Services.Implementations
{
    public class UserService(IConfiguration config) : IUserService
    {
        private readonly string _conn = PolmanAstraLibrary.PolmanAstraLibrary.Decrypt(config.GetConnectionString("DefaultConnection")!, Environment.GetEnvironmentVariable("DECRYPT_KEY_CONNECTION_STRING"));

        public async Task<(bool IsSuccess, List<Aplikasi> ListAplikasi, string? ErrorMessage)> AuthenticateAsync(string username, string jenisAplikasi)
        {
            try
            {
                var list = new List<Aplikasi>();

                await using var conn = new SqlConnection(_conn);
                await using var cmd = new SqlCommand("sso_getAppByUser", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@JenisAplikasi", jenisAplikasi);

                await conn.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    do
                    {
                        list.Add(new Aplikasi
                        {
                            NamaAplikasi = reader.GetString(reader.GetOrdinal("app_deskripsi")),
                            NamaRole = reader.GetString(reader.GetOrdinal("rol_deskripsi")),
                            Root = reader.GetString(reader.GetOrdinal("app_tautan")),
                            AppId = reader.GetString(reader.GetOrdinal("app_id")),
                            RoleId = reader.GetString(reader.GetOrdinal("rol_id")),
                        });
                    } while (await reader.ReadAsync());
                }
                return (true, list, "");
            }
            catch (Exception ex)
            {
                return (false, [], $"Gagal mendapatkan daftar aplikasi: {ex.Message}");
            }
        }

        public async Task<(bool IsSuccess, List<string> ListPermission, string? ErrorMessage)> GetPermissionAsync(string username, string aplikasi, string role)
        {
            try
            {
                var list = new List<string>();

                await using var conn = new SqlConnection(_conn);
                await using var cmd = new SqlCommand("sso_getListAkses", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Aplikasi", aplikasi);
                cmd.Parameters.AddWithValue("@Role", role);

                await conn.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    do
                    {
                        list.Add(reader.GetString(reader.GetOrdinal("permission")));
                    } while (await reader.ReadAsync());
                }
                return (true, list, "");
            }
            catch (Exception ex)
            {
                return (false, [], $"Gagal mendapatkan daftar hak akses: {ex.Message}");
            }
        }

        public async Task<bool> HasPermissionAsync(string username, string aplikasi, string role, string permission)
        {
            try
            {
                await using var conn = new SqlConnection(_conn);
                await using var cmd = new SqlCommand("sso_getAksesByUser", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Aplikasi", aplikasi);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@Permission", permission);

                await conn.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
