using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Model.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CRM.Inf
{
    public class MessageEventArgs
    {
        public string Message { get; private set; }
        public MessageEventArgs(string message)
        {
            Message = message;
        }
    }
    public class MQBro : IBroMessagerMQ
    {
        private const string HOST_NAME = "localhost";
        private const string ROUTING_KEY_SEND = "c.s";
        private const string ROUTING_KEY_RECEIVED = "c.q";

        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;

        public delegate void MessageEventHandler(object sender, MessageEventArgs e);

        public event MessageEventHandler MessageReceived;

        public MQBro(string host = HOST_NAME)
        {
            factory = new ConnectionFactory(){HostName = host};
        }

        public void Connection()
        {
            connection = factory.CreateConnection();
        }

        public void Publish(string message, string routKey = ROUTING_KEY_SEND)
        {
            if (connection == null)
                throw new NullReferenceException();
            if (channel == null)
                channel = connection.CreateModel();

            channel.QueueDeclare(queue: routKey,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: routKey,
                                 basicProperties: null,
                                 body: body);

            //Log : published 

        }

        public void Disconnection()
        {
            connection.Close();
        }

        public void Received(string routKey = ROUTING_KEY_RECEIVED)
        {
            if (connection == null)
                throw new NullReferenceException();
            if (channel == null)
                channel = connection.CreateModel();

            channel.QueueDeclare(queue: routKey,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body);
                if (MessageReceived != null)
                    MessageReceived(this,new MessageEventArgs(message));
            };

            channel.BasicConsume(queue: routKey,
                                 noAck: true,
                                 consumer: consumer);
        }
    }
}
