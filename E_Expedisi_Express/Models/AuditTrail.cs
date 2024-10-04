namespace E_Expedisi_Express.Models
{
    public class AuditTrail
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string UserName { get; set; }
        public string AreaAccessed { get; set; }
        public string TransactionDetails { get; set; }
        public DateTime Timestamp { get; set; }
        public string Role { get; set; }
        public string Company { get; set; }
    }
}
