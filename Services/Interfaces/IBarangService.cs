using Microsoft.Data.SqlClient;
using System.Data;

public class BarangService : IBarangService
{
	private readonly string _connectionString;

	public BarangService(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection");
	}

	// --- IMPLEMENTASI CREATE (Sudah kita buat) ---
	public async Task<int> AddBarangAsync(CreateBarangDto barangDto)
	{
		// ... (kode dari jawaban sebelumnya, yang memanggil usp_AddBarang) ...
		// (Salin-tempel kode lengkapnya di sini)
	}

	// --- IMPLEMENTASI READ (ALL) ---
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

	// --- IMPLEMENTASI READ (BY ID) ---
	public async Task<BarangReadDto> GetBarangByIdAsync(int idBarang)
	{
		// ... (Mirip dengan GetAll, tapi tambahkan parameter @ID_Barang) ...
		// ... (Ini bisa Anda kerjakan sebagai latihan) ...
	}

	// --- IMPLEMENTASI UPDATE ---
	public async Task UpdateBarangAsync(int idBarang, UpdateBarangDto barangDto)
	{
		// ... (Mirip dengan AddBarang, tapi panggil usp_UpdateBarang) ...
		// ... (Ingat untuk membuat DataTable untuk perlengkapan juga) ...
	}

	// --- IMPLEMENTASI DELETE ---
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