using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Pilot
{
    public class UpdatePilotDTO
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string LicenseNumber { get; set; }

        public int BranchId { get; set; }

        public bool IsActive { get; set; }
    }
}
