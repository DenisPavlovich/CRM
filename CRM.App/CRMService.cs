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
    public class CrmService : IServiceUseCase, IServiceStatuses, IServiceTakeQuery
    {
        private const string FILE_CONFIG = "conf.txt";
        private const string INVOKE = "INVOKE : ";

        private Timer timer = new Timer() {Interval = 2000};

        private MQBro _messengerMq;
        private RepositoryUnit _repositoryUnit;

        private List<string> statuses = new List<string>();

        public CrmService()
        {
            _messengerMq = new MQBro();
            _repositoryUnit = new RepositoryUnit(new DataBaseContext());
            timer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            PushStatuses();
        }

        public void Start()
        {
            timer.Start();
            _messengerMq.Connection();
            _messengerMq.MessageReceived += MessengerMqOnMessageReceived;
        }

        private void MessengerMqOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            object[] arr = JsonParser.Deserialize(messageEventArgs.Message);
            ChoiseMethod(arr);
        }

        public void Close()
        {
            _repositoryUnit.Dispose();
            _messengerMq.Disconnection();
        }

        public void AddOrganization(Dto obj)
        {
            if (obj is Organization)
            {
                _repositoryUnit.Oranizations.Add((Organization)obj);

                MakeStatus("AddOrganization", obj);
            }
        }

        public void AddClient(Dto obj)
        {
            if (obj is Client)
            {
                _repositoryUnit.Clients.Add((Client)obj);

                MakeStatus("AddClient", obj);
            }
        }

        public void EditClient(int id, string address = null, string discription = null, string email = null,
            string name = null)
        {
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
            var client = _repositoryUnit.Clients.Get(id);
            _repositoryUnit.Clients.Remove(client);
            client.PhoneNumber = new PhoneNumber() {Number = number};
            _repositoryUnit.Clients.Add(client);

            MakeStatus("AppendPhoneNumberToClient", client);
        }

        public void TakeManagerOwnerToClient(int id, Manager manager)
        {
            var client = _repositoryUnit.Clients.Get(id);
            _repositoryUnit.Clients.Remove(client);
            client.Manager = manager;
            _repositoryUnit.Clients.Add(client);

            MakeStatus("TakeManagerOwnerToClient",manager);
        }

        public void ChoiseMethod(object[] messArg)
        {
            MethodType method = (MethodType) messArg[0];
            Dto obj = (Dto)messArg[1];

            switch (method)
            {
                    case MethodType.AddClient:
                    AddClient(obj);
                    break;
                    case MethodType.AddOrganization:
                    AddOrganization(obj);
                    break;
                    case MethodType.AppendPhoneNumberToClient:
                    Client cl = (Client) obj;
                    AppendPhoneNumberToClient(obj.Id, cl.PhoneNumber.Number);
                    break;
                    case MethodType.EditClient:
                    cl = (Client) obj;
                    EditClient(obj.Id,address:cl.Address,discription:cl.Discription,email:cl.Email,name:cl.Name);
                    break;
                    case MethodType.TakeManagerOwnerToClient:
                    TakeManagerOwnerToClient(obj.Id, (Manager)obj);
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public void MakeStatus(string status, Dto obj)
        {
            DateTimeOffset time = new DateTimeOffset(DateTime.Now);
            string strTime = string.Format("{0:yyyy-MM-ddTHH:mm:sszzzZ}", time);
            statuses.Add(string.Format("Time : {0} --- INVOKE : {1} --- TYPE : {2}",strTime,status,obj.GetType().ToString()));
        }

        public void PushStatuses()
        {
            var str = JsonParser.BasicSerialize(statuses);
            _messengerMq.Publish(str);
            statuses.Clear();
        }

    }
}