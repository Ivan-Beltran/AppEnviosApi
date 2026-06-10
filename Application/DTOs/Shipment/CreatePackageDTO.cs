using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Shipment
{
    public class CreatePackageDTO
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }
    }
}
