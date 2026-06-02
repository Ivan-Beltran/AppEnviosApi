using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CompanyClient
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }
        public User User { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyPhone { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CompanyAddress { get; set; }
        [Required]
        public int DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public District District { get; set; }


    }
}
