using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Shipment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public Receiver Receiver { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime DeliveryDate { get; set; }


        public String DeliveryAddress { get; set; }

        public int DistrictDeliveryId { get; set; }
        [ForeignKey("DistrictDeliveryId")]
        public District DistrictDelivery { get; set; }

        public int CreatedByUserId { get; set; }
        [ForeignKey("CreatedByUserId")]
        public User CreatedByUser { get; set; }

        public int? PilotId { get; set; }

        [ForeignKey("PilotId")]

        public Pilot Pilot { get; set; }

        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public ShipmentStatus ShipmentStatus { get; set; }

    }
}
