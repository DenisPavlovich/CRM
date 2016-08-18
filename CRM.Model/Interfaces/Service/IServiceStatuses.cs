using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Data.Dto;

namespace CRM.Model.Interfaces.Service
{
    public interface IServiceStatuses
    {
        void MakeStatus(string status, Dto obj);
        void PushStatuses();
    }
}
