using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }
		[Required]
		public string BranchName { get; set; } = string.Empty;

		[Required]
		public string Address { get; set; } = string.Empty;

		[Required]
		public int DistrictId { get; set; }

		[ForeignKey("DistrictId")]
		public District? District { get; set; }
	}
}