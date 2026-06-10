using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Shipment
{
    public class UpdateShipmentDTO
    {
        public DateTime DeliveryDate { get; set; }

        public string DeliveryAddress { get; set; }

        public int DistrictDeliveryId { get; set; }

        public int? PilotId { get; set; }

        public int BranchId { get; set; }

        public int StatusId { get; set; }
    }
}
