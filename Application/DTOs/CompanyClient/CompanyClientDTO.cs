using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CompanyClient
{
    public class CompanyClientDTO
    {
        
        public int Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }

        public string UserPhone { get; set; }
        public string CompanyName { get; set; }
      
        public string CompanyPhone { get; set; }
       
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
      
        public string CompanyAddress { get; set; }

        public int DistricId { get; set; }
        public string? DistricName { get; set; }

    }
}
