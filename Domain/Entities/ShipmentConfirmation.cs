using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShipmentConfirmation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ShipmentId { get; set; }
        [ForeignKey("ShipmentId")]
        public Shipment Shipment { get; set; }
        [Required]
        public string ReceiverSignature { get; set; }
        [Required]
        public string ConfirmationPhoto { get; set; }
        [Required]
        public DateTime ConfirmationDate { get; set; }
    }
}
