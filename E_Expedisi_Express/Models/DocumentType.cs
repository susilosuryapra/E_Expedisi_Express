namespace E_Expedisi_Express.Models
{
    public class DocumentType
    {
        public int Id { get; set; }
        public string? Code { get; set; } // Mark as nullable
        public string? Description { get; set; } // Mark as nullable
        public bool Status { get; set; } = true; // Default value for active
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Default value
        public DateTime UpdatedAt { get; set; } = DateTime.Now; // Default value
    }
}
