using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using CRM.Data.Dto;
using CRM.Inf;
using CRM.Inf.RepositoryFiles;
using CRM.Model;
using CRM.Model.Domain;
using CRM.Model.Interfaces.Service;
using Client = CRM.Data.Dto.Client;
using Manager = CRM.Data.Dto.Manager;
using Organization = CRM.Data.Dto.Organization;
using PhoneNumber = CRM.Data.Dto.PhoneNumber;

namespace CRM.App
{
    public class CrmService : IServiceUseCase, IServiceStatuses
    {
        private const string FileConfig = "conf.txt";
        private const string Invoke = "INVOKE : ";
        private const int TimerInterval = 1000;

        private Timer _timer_send = new Timer() {Interval = TimerInterval};
        private Timer _timer_received = new Timer() { Interval = TimerInterval };

        private MqBro _messengerMq;
        private RepositoryUnit _repositoryUnit;

        private Queue<string> _statuses = new Queue<string>();

        public CrmService()
        {
            Console.WriteLine("CONSTRUCTOR");

            _messengerMq = new MqBro();
            _repositoryUnit = new RepositoryUnit(new DataBaseContext());
            _timer_send.Elapsed += timer_Elapsed;
            _timer_received.Elapsed += _timer_received_Elapsed;  
        }

        void _timer_received_Elapsed(object sender, ElapsedEventArgs e)
        {
            _messengerMq.Received();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("TIMER TIMER TIMER");
            Console.WriteLine(_statuses.Count);
            PushStatuses();
        }

        public void Start()
        {
            Console.WriteLine("Start");

            _timer_send.Start();
            _timer_received.Start();
            _messengerMq.Connection();
            _messengerMq.MessageReceived += MessengerMqOnMessageReceived;
        }

        private void MessengerMqOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            Console.WriteLine("MessengerMqOnMessageReceived");
            var str = messageEventArgs.Message;
            Console.WriteLine(str);

            //////////WARNING/////////////

            JsonParser jp = JsonParser.Deserialize(str);
            Dto obj;
            switch (jp.Method)
            {
                    case MethodType.AddClient:
                    //obj = JsonParser.BasicDeserialize<Client>(jp.Obj);
                    obj = jp.Obj;
                    AddClient(obj);
                    break;
                    case MethodType.AddOrganization:
                    obj = jp.Obj;
                    AddOrganization(obj);
                    break;
                    case MethodType.AppendPhoneNumberToClient:
                    obj = jp.Obj;
                    AppendPhoneNumberToClient(obj.Id,((Client)obj).PhoneNumber.Number);
                    break;
                    case MethodType.EditClient:
                    obj = jp.Obj;
                    Client cl = (Client) obj;
                    EditClient(obj.Id,address:cl.Address,discription:cl.Discription,email:cl.Email,name:cl.Name);
                    break;
                    case MethodType.TakeManagerOwnerToClient:
                    obj = jp.Obj;
                    TakeManagerOwnerToClient(obj.Id, ((Client)obj).Manager);
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public void Stop()
        {
            Console.WriteLine("Stop");

            _repositoryUnit.Dispose();
            _messengerMq.Disconnection();
        }

        public void AddOrganization(Dto obj)
        {
            Console.WriteLine("AddOrganization");

            if (obj is Organization)
            {
                _repositoryUnit.Oranizations.Add((Organization)obj);

                MakeStatus("AddOrganization", obj);
            }
        }

        public void AddClient(Dto obj)
        {
            Console.WriteLine("AddClient");

            if (obj is Client)
            {
                _repositoryUnit.Clients.Add((Client)obj);

                MakeStatus("AddClient", obj);
            }
        }

        public void EditClient(int id, string address = null, string discription = null, string email = null,
            string name = null)
        {
            Console.WriteLine("EditClient");

            var client = _repositoryUnit.Clients.Get(id);
            _repositoryUnit.Clients.Remove(client);
            if (address != null) client.Address = address;
            if (discription != null) client.Discription = discription;
            if (email != null) client.Email = email;
            if (name != null) client.Name = name;
            _repositoryUnit.Clients.Add(client);

            MakeStatus("EditClient", client);
        }

        public void AppendPhoneNumberToClient(int id, string number)
        {
            Console.WriteLine("AppendPhoneNumberToClient");

            var client = _repositoryUnit.Clients.Get(id);
            _repositoryUnit.Clients.Remove(client);
            client.PhoneNumber = new PhoneNumber() {Number = number};
            _repositoryUnit.Clients.Add(client);

            MakeStatus("AppendPhoneNumberToClient", client);
        }

        public void TakeManagerOwnerToClient(int id, Manager manager)
        {
            Console.WriteLine("TakeManagerOwnerToClient");

            var client = _repositoryUnit.Clients.Get(id);
            _repositoryUnit.Clients.Remove(client);
            client.Manager = manager;
            _repositoryUnit.Clients.Add(client);

            MakeStatus("TakeManagerOwnerToClient",manager);
        }

        public void MakeStatus(string status, Dto obj)
        {
            Console.WriteLine("MakeStatus");

            DateTimeOffset time = new DateTimeOffset(DateTime.Now);
            string strTime = string.Format("{0:yyyy-MM-ddTHH:mm:sszzzZ}", time);
            _statuses.Enqueue(string.Format("Time : {0} --- INVOKE : {1} --- TYPE : {2}",strTime,status,obj.GetType().ToString()));
        }

        public void PushStatuses()
        {
            Console.WriteLine("PushStatuses");

            if (_statuses.Count > 0)
            {
                var str = _statuses.Dequeue();
                Console.WriteLine("SEND : " + str);
                _messengerMq.Publish(str);
            }
        }
    }
}