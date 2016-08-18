using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Data.Dto;

namespace CRM.Model.Domain
{
    public class Client
    {
        public Client() { }

        public Client(Dto obj)
        {
            if (obj is CRM.Data.Dto.Client)
            {
                CRM.Data.Dto.Client client = (CRM.Data.Dto.Client)obj;
                Address = client.Address;
                Discription = client.Discription;
                Email = client.Email;
                Name = client.Name;
            }
            throw new ArgumentException();
        }
        public PhoneNumber PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Discription { get; set; }
        public Organization Organization { get; set; }
        public string Email { get; set; }
        public Manager Manager { get; set; }
    }
}
