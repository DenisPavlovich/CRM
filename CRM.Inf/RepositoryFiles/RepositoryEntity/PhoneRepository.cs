using System;
using CRM.Data.Dto;
using CRM.Model.Interfaces;

namespace CRM.Inf.RepositoryFiles.RepositoryEntity
{
    public class PhoneRepository : Repository<PhoneNumber>, IPhoneRepository, IDisposable
    {
        public PhoneRepository(DataBaseContext context)
            : base(context)
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
