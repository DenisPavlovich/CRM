using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Data.Dto;
using CRM.Model.Domain;

namespace CRM.Model.Interfaces.Service
{
    public interface IServiceTakeQuery
    {
        void ChoiseMethod(Dto messArg, MethodType addClient);
    }
}
