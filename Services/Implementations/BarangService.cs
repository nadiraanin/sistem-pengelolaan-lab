// File: Services/BarangService.cs

using Microsoft.Data.SqlClient;
using System.Data;

public class BarangService : IBarangService
{
    private readonly string _connectionString;

    public BarangService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    // ==========================================================
    // BAGIAN CREATE - Kode ini sudah Anda miliki dan sudah benar
    // ==========================================================
    public async Task<int> AddBarangAsync(CreateBarangDto barangDto)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("dbo.usp_AddBarang", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Nama_Barang", barangDto.NamaBarang);
            command.Parameters.AddWithValue("@Jenis_Barang", barangDto.JenisBarang);
            command.Parameters.AddWithValue("@Stok_Barang", barangDto.StokBarang);
            command.Parameters.AddWithValue("@Serial_Number", (object)barangDto.SerialNumber ?? DBNull.Value);
            command.Parameters.AddWithValue("@ID_Storage", (object)barangDto.IdStorage ?? DBNull.Value);

            var perlengkapanTable = new DataTable();
            perlengkapanTable.Columns.Add("Nama_Perlengkapan", typeof(string));
            foreach (var nama in barangDto.NamaPerlengkapanList)
            {
                perlengkapanTable.Rows.Add(nama);
            }
            var perlengkapanParam = command.Parameters.AddWithValue("@PerlengkapanList", perlengkapanTable);
            perlengkapanParam.SqlDbType = SqlDbType.Structured;
            perlengkapanParam.TypeName = "dbo.PerlengkapanType";

            var newIdParam = new SqlParameter("@NewBarangID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(newIdParam);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            int newBarangId = Convert.ToInt32(newIdParam.Value);
            return newBarangId;
        }
    }

    // ==========================================================
    // BAGIAN BARU #1: Tambahkan implementasi untuk READ ALL
    // ==========================================================
    public async Task<IEnumerable<BarangReadDto>> GetAllBarangAsync()
    {
        var barangList = new List<BarangReadDto>();
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("dbo.usp_ReadBarang", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            await connection.OpenAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    barangList.Add(new BarangReadDto
                    {
                        IdBarang = reader.GetInt32(reader.GetOrdinal("ID_Barang")),
                        NamaBarang = reader.GetString(reader.GetOrdinal("Nama_Barang")),
                        StokBarang = reader.GetInt32(reader.GetOrdinal("Stok_Barang")),
                        StatusBarang = reader.GetString(reader.GetOrdinal("Status_Barang")),
                        JenisBarang = reader.GetString(reader.GetOrdinal("Jenis_Barang")),
                        SerialNumber = reader.IsDBNull(reader.GetOrdinal("Serial_Number")) ? null : reader.GetString(reader.GetOrdinal("Serial_Number")),
                        KondisiBarang = reader.GetString(reader.GetOrdinal("Kondisi_Barang")),
                        NamaStorage = reader.GetString(reader.GetOrdinal("Nama_Storage")),
                        NamaRuangan = reader.GetString(reader.GetOrdinal("Nama_Ruangan")),
                        DaftarPerlengkapan = reader.GetString(reader.GetOrdinal("Daftar_Perlengkapan"))
                    });
                }
            }
        }
        return barangList;
    }

    // ==========================================================
    // BAGIAN BARU #2: Tambahkan implementasi untuk READ BY ID
    // ==========================================================
    public async Task<BarangReadDto> GetBarangByIdAsync(int idBarang)
    {
        BarangReadDto barang = null;
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("dbo.usp_ReadBarang", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@ID_Barang", idBarang);

            await connection.OpenAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    barang = new BarangReadDto
                    {
                        // (Mapping kolom sama seperti di GetAllBarangAsync)
                        IdBarang = reader.GetInt32(reader.GetOrdinal("ID_Barang")),
                        NamaBarang = reader.GetString(reader.GetOrdinal("Nama_Barang")),
                        StokBarang = reader.GetInt32(reader.GetOrdinal("Stok_Barang")),
                        StatusBarang = reader.GetString(reader.GetOrdinal("Status_Barang")),
                        JenisBarang = reader.GetString(reader.GetOrdinal("Jenis_Barang")),
                        SerialNumber = reader.IsDBNull(reader.GetOrdinal("Serial_Number")) ? null : reader.GetString(reader.GetOrdinal("Serial_Number")),
                        KondisiBarang = reader.GetString(reader.GetOrdinal("Kondisi_Barang")),
                        NamaStorage = reader.GetString(reader.GetOrdinal("Nama_Storage")),
                        NamaRuangan = reader.GetString(reader.GetOrdinal("Nama_Ruangan")),
                        DaftarPerlengkapan = reader.GetString(reader.GetOrdinal("Daftar_Perlengkapan"))
                    };
                }
            }
        }
        return barang;
    }

    // ==========================================================
    // BAGIAN BARU #3: Tambahkan implementasi untuk UPDATE
    // ==========================================================
    public async Task UpdateBarangAsync(int idBarang, UpdateBarangDto barangDto)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("dbo.usp_UpdateBarang", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ID_Barang", idBarang);
            command.Parameters.AddWithValue("@Nama_Barang", barangDto.NamaBarang);
            command.Parameters.AddWithValue("@Jenis_Barang", barangDto.JenisBarang);
            command.Parameters.AddWithValue("@Stok_Barang", barangDto.StokBarang);
            command.Parameters.AddWithValue("@Kondisi_Barang", barangDto.KondisiBarang);
            command.Parameters.AddWithValue("@Serial_Number", (object)barangDto.SerialNumber ?? DBNull.Value);
            command.Parameters.AddWithValue("@ID_Storage", (object)barangDto.IdStorage ?? DBNull.Value);

            var perlengkapanTable = new DataTable();
            perlengkapanTable.Columns.Add("Nama_Perlengkapan", typeof(string));
            foreach (var nama in barangDto.NamaPerlengkapanList)
            {
                perlengkapanTable.Rows.Add(nama);
            }
            var perlengkapanParam = command.Parameters.AddWithValue("@PerlengkapanList", perlengkapanTable);
            perlengkapanParam.SqlDbType = SqlDbType.Structured;
            perlengkapanParam.TypeName = "dbo.PerlengkapanType";

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }

    // ==========================================================
    // BAGIAN BARU #4: Tambahkan implementasi untuk DELETE
    // ==========================================================
    public async Task SoftDeleteBarangAsync(int idBarang)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("dbo.usp_SoftDeleteBarang", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@ID_Barang", idBarang);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}