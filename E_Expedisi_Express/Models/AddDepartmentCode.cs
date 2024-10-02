using System.ComponentModel.DataAnnotations;

namespace E_Expedisi_Express.Models
{
    public class AddDepartmentCode
    {
        [Required(ErrorMessage = "Please provide a valid department name")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Please provide a valid code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please provide a valid description")]
        public string Description { get; set; }
    }
}
