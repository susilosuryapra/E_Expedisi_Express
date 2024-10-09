using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Expedisi_Express.Models
{
    [Table("DepartmentCode")]
    public class DepartmentCodeModel
    {
        [Key]  // Menandakan bahwa Id adalah Primary Key
        public int Id { get; set; }  // Primary Key bertipe int

        public string NewId { get; set; } // Renamed and set as string

        [Required(ErrorMessage = "Please provide a department name.")]
        public string DepartmentName { get; set; }  // Renamed from 'Name'

        [Required(ErrorMessage = "Please provide a department code.")]
        public string DepartmentCode { get; set; }  // Renamed from 'Code'

        [Required(ErrorMessage = "Please provide a description.")]
        public string Description { get; set; }

        public string? CompCode { get; set; }  // Added field
        public string? DivCode { get; set; }   // Added field

        [Required(ErrorMessage = "Please provide a status.")]
        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
