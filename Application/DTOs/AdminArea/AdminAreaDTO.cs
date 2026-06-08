using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AdminArea
{
	public class AdminAreaDTO
	{
		public int UserId { get; set; }
		public string? FullName { get; set; }

		public int BranchId { get; set; }
		public string? BranchName { get; set; }

		public bool IsActive { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}

