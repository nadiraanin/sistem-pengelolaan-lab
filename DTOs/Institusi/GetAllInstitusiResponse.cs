namespace sistem_pengelolaan_lab.DTOs.Institusi
{
    public class GetAllInstitusiResponse
    {
        public List<InstitusiDto> Data { get; set; } = [];
        public int TotalData { get; set; } = 0;
        public int TotalHalaman { get; set; } = 0;
    }
}
