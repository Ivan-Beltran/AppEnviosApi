using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AdminArea
{
	public class UpdateAdminAreaDTO
	{
        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
        public int BranchId { get; set; }

		
	}
}
