using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data.Dto
{
    public class Client : Dto
    {
        public int PhoneId { get; set; }
        public virtual PhoneNumber PhoneNumber { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Discription { get; set; }

        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

        public string Email { get; set; }

        public int ManagerId { get; set; }
        public virtual Manager Manager { get; set; }
    }
}
