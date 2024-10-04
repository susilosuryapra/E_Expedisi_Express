using System.ComponentModel.DataAnnotations;

namespace E_Expedisi_Express.Models
{
    public class DepartmentCode
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a department name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide a code.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please provide a description.")]
        public string Description { get; set; }
    }

}
