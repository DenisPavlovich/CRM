using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Model.Interfaces;
using CRM.Inf.RepositoryFiles;
using CRM.Inf.RepositoryFiles.RepositoryEntity;

namespace CRM.Inf
{
    public class RepositoryUnit : IRepositoryUnit, IDisposable
    {
        private readonly DataBaseContext _context;

        public RepositoryUnit(DataBaseContext context)
        {
            _context = context;
            Phones = new PhoneRepository(_context);
            Oranizations = new OraganizationRepository(_context);
            Managers = new ManagerRepository(_context);
            Clients = new ClientsRepository(_context);
        }
        public IClientRepository Clients { get; private set; }
        public IPhoneRepository Phones { get; private set; }
        public IOrganizationRepository Oranizations { get; private set; }
        public IManagerRepository Managers { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
