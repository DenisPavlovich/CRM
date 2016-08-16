using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CRM.Data.Dto;
using CRM.Model.Interfaces;

namespace CRM.Inf.RepositoryFiles
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
