// File: BarangService.cs

using Microsoft.Data.SqlClient;
using System.Data;

public class BarangService : IBarangService // Asumsikan Anda punya interface IBarangService
{
	private readonly string _connectionString;

	// Ambil connection string dari file appsettings.json
	public BarangService(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection");
	}

	public async Task<int> AddBarangAsync(CreateBarangDto barangDto)
	{
		using (var connection = new SqlConnection(_connectionString))
		{
			// 1. SIAPKAN COMMAND UNTUK MEMANGGIL STORED PROCEDURE
			var command = new SqlCommand("dbo.usp_AddBarang", connection)
			{
				CommandType = CommandType.StoredProcedure
			};

			// 2. TAMBAHKAN PARAMETER INPUT BIASA
			command.Parameters.AddWithValue("@Nama_Barang", barangDto.NamaBarang);
			command.Parameters.AddWithValue("@Jenis_Barang", barangDto.JenisBarang);
			command.Parameters.AddWithValue("@Stok_Barang", barangDto.StokBarang);

			// Cara yang benar untuk menangani parameter yang bisa NULL
			command.Parameters.AddWithValue("@Serial_Number", (object)barangDto.SerialNumber ?? DBNull.Value);
			command.Parameters.AddWithValue("@ID_Storage", (object)barangDto.IdStorage ?? DBNull.Value);

			// 3. SIAPKAN TABLE-VALUED PARAMETER (TVP) UNTUK PERLENGKAPAN
			// Buat DataTable di C# yang strukturnya sama persis dengan 'dbo.PerlengkapanType'
			var perlengkapanTable = new DataTable();
			perlengkapanTable.Columns.Add("Nama_Perlengkapan", typeof(string));

			// Isi DataTable dengan data dari List
			foreach (var nama in barangDto.NamaPerlengkapanList)
			{
				perlengkapanTable.Rows.Add(nama);
			}

			// Tambahkan DataTable sebagai parameter khusus
			var perlengkapanParam = command.Parameters.AddWithValue("@PerlengkapanList", perlengkapanTable);
			perlengkapanParam.SqlDbType = SqlDbType.Structured; // Wajib untuk TVP
			perlengkapanParam.TypeName = "dbo.PerlengkapanType";  // Nama tipe kustom di SQL Server

			// 4. SIAPKAN PARAMETER OUTPUT UNTUK MENANGKAP ID BARU
			var newIdParam = new SqlParameter("@NewBarangID", SqlDbType.Int)
			{
				Direction = ParameterDirection.Output
			};
			command.Parameters.Add(newIdParam);

			// 5. BUKA KONEKSI DAN EKSEKUSI
			await connection.OpenAsync();
			await command.ExecuteNonQueryAsync();

			// 6. AMBIL NILAI DARI PARAMETER OUTPUT
			int newBarangId = Convert.ToInt32(newIdParam.Value);

			return newBarangId;
		}
	}
}