using System.ComponentModel.DataAnnotations;

namespace E_Expedisi_Express.Models
{
    public class DepartmentCode
    {
        public int Id { get; set; }

        public Guid? NewId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Please provide a department name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide a code.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please provide a description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please provide a status.")]
        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

}
