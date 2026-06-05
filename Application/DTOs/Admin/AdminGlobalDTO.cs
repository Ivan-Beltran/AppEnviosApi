using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admin
{
    public class AdminGlobalDTO
    {
        public UserDto User {  get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

    }
}
