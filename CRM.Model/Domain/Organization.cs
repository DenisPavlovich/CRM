﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Model.Domain
{
    class Organization
    {
        public string Discription { get; set; }
        public string Address { get; set; }
        public Organization Org { get; set; }
    }
}
