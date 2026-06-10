using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Shipment
{
    public class ShipmentDTO
    {
        public int Id { get; set; }

        public string ReceiverName { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string DeliveryAddress { get; set; }

        public string DistrictDeliveryName { get; set; }

        public string CreatedByUserName { get; set; }

        public string? PilotName { get; set; }

        public string BranchName { get; set; }

        public string StatusName { get; set; }


    }
}
