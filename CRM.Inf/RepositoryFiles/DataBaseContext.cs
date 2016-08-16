using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Data.Dto;

namespace CRM.Inf.RepositoryFiles
{
    public class DataBaseContext : DbContext
    {
        public DbSet<CRM.Data.Dto.PhoneNumber> Phones { get; set; }
        public DbSet<CRM.Data.Dto.Client> Clients { get; set; }
        public DbSet<CRM.Data.Dto.Manager> Managers { get; set; }
        public DbSet<CRM.Data.Dto.Organization> Organizations { get; set; }
    }
}
