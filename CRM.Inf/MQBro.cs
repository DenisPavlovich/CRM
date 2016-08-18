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
    public class MqBro : IBroMessagerMq
    {
        private const string HostName = "localhost";
        private const string ROUTING_KEY_CS = "c.s";
        private const string ROUTING_KEY_CQ = "c.q";

        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        private EventingBasicConsumer _consumer;

        public delegate void MessageEventHandler(object sender, MessageEventArgs e);

        public event MessageEventHandler MessageReceived;

        public MqBro(string host = HostName)
        {
            _factory = new ConnectionFactory(){HostName = host};
        }

        public void Connection()
        {
            _connection = _factory.CreateConnection();
        }

        public void Publish(string message, string routKey = ROUTING_KEY_CS)
        {
            if (_connection == null)
                throw new NullReferenceException();
            if (_channel == null)
                _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: routKey,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                 routingKey: routKey,
                                 basicProperties: null,
                                 body: body);

            //Log : published 

        }

        public void Disconnection()
        {
            _connection.Close();
        }

        public void Received(string routKey = ROUTING_KEY_CQ)
        {
            if (_connection == null)
                throw new NullReferenceException();
            if (_channel == null)
                _channel = _connection.CreateModel();
            if (_consumer == null)
                _consumer = new EventingBasicConsumer(_channel);

            _channel.QueueDeclare(queue: routKey,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body);
                if (MessageReceived != null)
                    MessageReceived(this,new MessageEventArgs(message));
            };

            _channel.BasicConsume(queue: routKey,
                                 noAck: true,
                                 consumer: _consumer);
        }
    }
}
