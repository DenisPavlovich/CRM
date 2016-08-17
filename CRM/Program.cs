using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Data.Dto;
using CRM.Inf;
using CRM.Inf.RepositoryFiles;

namespace CRM
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var unit = new RepositoryUnit(new DataBaseContext()))
            {
                //unit.Phones.Add(new PhoneNumber(){Number = "380931399390"});
                //unit.Complete();
                var phone = unit.Phones.GetAll();
                foreach (var number in phone)
                {
                    Console.WriteLine(number.Number);
                }
            }
        }
    }
}
