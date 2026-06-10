using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Pilot
{
    public class PilotDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string LicenseNumber { get; set; }

        public int BranchId { get; set; }
        public string? BranchName { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
