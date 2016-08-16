using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Model.Domain
{
    class Client
    {
        public PhoneNumber PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Discription { get; set; }
        public Organization Organization { get; set; }
        public string Email { get; set; }
        public Manager Manager { get; set; }
    }
}
