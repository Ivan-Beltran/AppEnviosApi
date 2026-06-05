using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UpdateUserDTO
    {
        public string FullName { get; set; }

        public string Phone { get; set; }

        public String Email { get; set; }

        public bool IsActive { get; set; }

    }
}
