using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Data.Dto;

namespace CRM.Model.Domain
{
    public class Organization
    {
        public Organization() { }
        public Organization(Dto obj)
        {
            if (obj is CRM.Data.Dto.Organization)
            {
                CRM.Data.Dto.Organization organization = (CRM.Data.Dto.Organization)obj;
                Address = organization.Address;
                Discription = organization.Discription;
            }
            throw new ArgumentException();
        }
        public string Discription { get; set; }
        public string Address { get; set; }
        public Organization Org { get; set; }
    }
}
