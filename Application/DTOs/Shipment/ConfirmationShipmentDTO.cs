using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Shipment
{

    public class ConfirmationShipmentDTO
    {
        public int ShipmentId { get; set; }
        public string ReceiverSignature { get; set; } = string.Empty;
        public string ConfirmationPhoto { get; set; } = string.Empty;
    }
        

}
