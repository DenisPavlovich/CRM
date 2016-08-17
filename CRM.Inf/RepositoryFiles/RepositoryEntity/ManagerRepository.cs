using System.Data.Entity;
using CRM.Data.Dto;
using CRM.Model.Interfaces;

namespace CRM.Inf.RepositoryFiles.RepositoryEntity
{
    class ManagerRepository : Repository<Manager>, IManagerRepository
    {
        public ManagerRepository(DbContext context)
            : base(context)
        {
        }
    }
}
