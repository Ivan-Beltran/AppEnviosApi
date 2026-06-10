using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Shipment
{
    public class CreateReturnDTO
    {
        public int ShipmentId { get; set; }

        public string ReasonReturn { get; set; }
    }
}
