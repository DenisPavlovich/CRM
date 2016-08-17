using System;
using System.Data.Entity;
using CRM.Data.Dto;
using CRM.Model.Interfaces;

namespace CRM.Inf.RepositoryFiles.RepositoryEntity
{
    class ClientsRepository : Repository<Client>, IClientRepository, IDisposable
    {
        public ClientsRepository(DbContext context)
            : base(context)
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
