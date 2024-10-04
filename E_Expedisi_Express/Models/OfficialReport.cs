using System.ComponentModel.DataAnnotations;

namespace E_Expedisi_Express.Models
{
    public class OfficialReport
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a Title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please provide a Giver.")]
        public string Giver { get; set; }

        [Required(ErrorMessage = "Please provide a Receiver.")]
        public string Receiver { get; set; }

        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please provide a Status.")]
        public string Status { get; set; }
    }
}
