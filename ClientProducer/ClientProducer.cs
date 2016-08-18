using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CRM.Data.Dto;
using CRM.Inf;
using CRM.Model;

namespace ClientProducer
{
    class ClientProducer
    {
        private const int Sleep = 1500;
        private const string RoutingKey = "c.q";
        static void Main(string[] args)
        {
            MqBro rabbitMq = new MqBro();
            rabbitMq.Connection();
            JsonParser jp = new JsonParser(new Organization(){Address = "aaaa", Discription = "BBBB"}, MethodType.AddOrganization);
            var json = JsonParser.Serialize(jp);
            while (true)
            {
                Thread.Sleep(Sleep);
                rabbitMq.Publish(json,RoutingKey);
                Console.WriteLine(json);

            }
            rabbitMq.Disconnection();
        }
    }
}
