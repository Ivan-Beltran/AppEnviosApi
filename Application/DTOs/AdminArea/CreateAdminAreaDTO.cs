using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AdminArea
{
	public class CreateAdminAreaDTO
	{
		[Required]
		public int UserId { get; set; } 

		[Required]
		public int BranchId { get; set; }
	}
}
