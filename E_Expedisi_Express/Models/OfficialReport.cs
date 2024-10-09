using System;
using System.ComponentModel.DataAnnotations;

namespace E_Expedisi_Express.Models
{
    public class OfficialReport
    {
        public int Id { get; set; }
        public Guid? NewId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Please provide a title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please provide a giver.")]
        public string Giver { get; set; }

        [Required(ErrorMessage = "Please provide a receiver.")]
        public string Receiver { get; set; }

        [Required(ErrorMessage = "Please provide a date.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please provide a status.")]
        public string Status { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
