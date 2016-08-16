using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Model.Interfaces
{
    public interface IRepositoryUnit
    {
        IClientRepository Clients { get; }
        IPhoneRepository Phones { get; }
        IOrganizationRepository Oranizations { get; }
        IManagerRepository Managers { get; }

        int Complete();
    }
}
