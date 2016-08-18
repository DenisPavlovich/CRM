using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Data.Dto;

namespace CRM.Model.Domain
{
    public enum PhoneNumberType
    {
        Work = 1,
        Home = 2,
        Mobile = 3
    }
    public class PhoneNumber
    {
        public PhoneNumber() { }
        public PhoneNumber(Dto obj)
        {
            if (obj is CRM.Data.Dto.PhoneNumber)
            {
                CRM.Data.Dto.PhoneNumber phoneNumber = (CRM.Data.Dto.PhoneNumber)obj;
                Number = phoneNumber.Number;
            }
            throw new ArgumentException();
        }
        public string Number { get; set; }

    }
}
