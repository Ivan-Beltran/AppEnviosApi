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
        public string FullName { get; set; }

        public string Phone { get; set; }

        public String Email { get; set; }

        public string Password { get; set; }

        public int BranchId { get; set; }
    }
}
