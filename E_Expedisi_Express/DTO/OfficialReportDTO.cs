namespace E_Expedisi_Express.DTO
{
    public class OfficialReportDTO
    {
        public int Id { get; set; }
        public string? NewId { get; set; }
        public string ReportTitle { get; set; }
        public string? ReportNumber { get; set; }
        public int DocumentTypeId { get; set; }
        public int DepartmentId { get; set; }
        public string GiverName { get; set; }
        public string GiverCompanyName { get; set; }
        public string GiverDepartmentName { get; set; }
        public string GiverAddress { get; set; }
        public string MainDescription { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverCompanyName { get; set; }
        public string ReceiverDepartmentName { get; set; }
        public string ReceiverAddress { get; set; }
        public string? CompCode { get; set; }
        public string? DivCode { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
    }
}
