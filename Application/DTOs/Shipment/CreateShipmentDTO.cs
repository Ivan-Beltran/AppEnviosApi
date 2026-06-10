using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Shipment
{
    public class CreateShipmentDTO
    {
        public int ReceiverId { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string DeliveryAddress { get; set; }

        public int DistrictDeliveryId { get; set; }

        public int BranchId { get; set; }

        public List<CreatePackageDTO> Packages { get; set; } = new();

    }
}
