using System.Data.Entity;
using CRM.Data.Dto;
using CRM.Model.Interfaces;

namespace CRM.Inf.RepositoryFiles.RepositoryEntity
{
    class OraganizationRepository : Repository<Organization>, IOrganizationRepository
    {
        public OraganizationRepository(DbContext context)
            : base(context)
        {
        }
    }
}
