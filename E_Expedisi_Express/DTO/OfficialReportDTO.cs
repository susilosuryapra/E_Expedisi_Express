namespace E_Expedisi_Express.DTO
{
    public class OfficialReportDTO
    {
        public int Id { get; set; }
        public Guid? NewId { get; set; }
        public string Title { get; set; }
        public string Giver { get; set; }
        public string Receiver { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
