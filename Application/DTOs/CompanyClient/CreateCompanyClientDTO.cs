using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CompanyClient
{
    public class CreateCompanyClientDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string UserPhone { get; set; }
        public string CompanyName { get; set; }

        public string CompanyPhone { get; set; }


        public string CompanyAddress { get; set; }

        public int DistricId { get; set; }
    }
}
