using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using CRM.App;
using CRM.Data.Dto;
using CRM.Inf;
using CRM.Inf.RepositoryFiles;
using CRM.Model;

namespace CRM
{
    class Program
    {
        private const int SLEEP = 500;
        static void Main(string[] args)
        {
            CrmService service = new CrmService();
            service.Start();
            //service.MessengerMq.MessageReceived += _messengerMq_MessageReceived;
            while (true)
            {
                Thread.Sleep(SLEEP);
                //service.MessengerMq.Received();
            }
            service.Stop();
        }

        //static void _messengerMq_MessageReceived(object sender, MessageEventArgs e)
        //{
        //    Console.WriteLine("CRM..Received...");
        //    Console.WriteLine(e.Message);
        //}
    }
}