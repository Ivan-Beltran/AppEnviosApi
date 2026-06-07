using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Pilot
{
    public class ConfirmDeliveryDTO
    {
        [Required]
        public string ReceiverSignature { get; set; }

        [Required]
        public string ConfirmationPhoto { get; set; }
    }
}