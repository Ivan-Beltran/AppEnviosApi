using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BranchDTO
    {
		public int Id { get; set; }

		public string BranchName { get; set; } = string.Empty;

		public string Address { get; set; } = string.Empty;

		public int DistrictId { get; set; }

		public string DistrictName { get; set; } = string.Empty;

		public string DepartmentName { get; set; } = string.Empty;

    }
}
