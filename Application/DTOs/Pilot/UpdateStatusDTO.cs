using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Pilot
{
    public class UpdateStatusDTO
    {
        [Required]
        public int StatusId { get; set; }
    }
}