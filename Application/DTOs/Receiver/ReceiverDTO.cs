using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Receiver
{
    public class ReceiverDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
    
        public string Address { get; set; }
  
        public int CreatedByUserId { get; set; }

        public string CreatedByUserFullName { get; set; }

        public int DistrictId { get; set; }

        public string DistrictName { get; set; }
    }
}
