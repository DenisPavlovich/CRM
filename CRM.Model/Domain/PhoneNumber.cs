using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Number { get; set; }

    }
}
