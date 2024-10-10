using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Expedisi_Express.Models
{
    [Table("OfficialReport")]
    public class OfficialReport
    {
        [Key]  // Menandakan bahwa Id adalah Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // BIAR OTOMATIS KEISI
        public int Id { get; set; }

        // Automatically generate Guid for NewId
        public Guid? NewId { get; set; }

        [Required(ErrorMessage = "Please provide a report title.")]
        public string ReportTitle { get; set; }

        [Required(ErrorMessage = "Please provide a report number.")]
        public string ReportNumber { get; set; }

        [Required(ErrorMessage = "Please provide a document type.")]
        public int DocumentTypeId { get; set; }

        [Required(ErrorMessage = "Please provide a department ID.")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Please provide the giver's name.")]
        public string GiverName { get; set; }

        [Required(ErrorMessage = "Please provide the giver's company name.")]
        public string GiverCompanyName { get; set; }

        public string GiverDepartmentName { get; set; }

        [Required(ErrorMessage = "Please provide the giver's address.")]
        public string GiverAddress { get; set; }

        [Required(ErrorMessage = "Please provide a main description.")]
        public string MainDescription { get; set; }

        [Required(ErrorMessage = "Please provide the receiver's name.")]
        public string ReceiverName { get; set; }

        [Required(ErrorMessage = "Please provide the receiver's company name.")]
        public string ReceiverCompanyName { get; set; }

        public string ReceiverDepartmentName { get; set; }

        [Required(ErrorMessage = "Please provide the receiver's address.")]
        public string ReceiverAddress { get; set; }

        public string CompCode { get; set; }

        public string DivCode { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
    }
}
