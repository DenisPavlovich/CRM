using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Inf;
using System.Threading;

namespace StatusToFileProducer
{
    class StatusToFileConsumer
    {

        private const int Sleep = 500;
        private const string RoutingKey = "c.s";

        static void Main(string[] args)
        {
            MqBro rabbitMq = new MqBro();
            rabbitMq.Connection();
            rabbitMq.MessageReceived += rabbitMq_MessageReceived;
            while (true)
            {
                Thread.Sleep(Sleep);
                rabbitMq.Received(RoutingKey);

            }
            rabbitMq.Disconnection();
        }

        static void rabbitMq_MessageReceived(object sender, MessageEventArgs e)
        {
            Console.WriteLine(e.Message);
            File.AppendAllText(Environment.CurrentDirectory + @"\Statuses.txt", e.Message+"\n");
        }
    }
}
