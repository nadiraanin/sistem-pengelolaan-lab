public class CreateBarangDto
{
	public string NamaBarang { get; set; }
	public string JenisBarang { get; set; }
	public int StokBarang { get; set; }
	public string? SerialNumber { get; set; }
	public int? IdStorage { get; set; }
	public List<string> NamaPerlengkapanList { get; set; } = new List<string>();
}