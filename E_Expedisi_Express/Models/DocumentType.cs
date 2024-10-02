namespace E_Expedisi_Express.Models
{
    public class DocumentType
    {
        public int Id { get; set; }
        public string? Code { get; set; } // Mark as nullable
        public string? Description { get; set; } // Mark as nullable
        public bool Status { get; set; } = true; // Default value for active
        public DateTime CreatedDate { get; set; } = DateTime.Now; // Default value
        public DateTime UpdatedDate { get; set; } = DateTime.Now; // Default value
        public string? CreatedBy { get; set; } // New property for CreatedBy
        public string? UpdatedBy { get; set; } // New property for UpdatedBy
    }
}
