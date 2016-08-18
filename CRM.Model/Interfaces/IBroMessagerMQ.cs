using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Model.Interfaces
{
    public interface IBroMessagerMq
    {
        void Connection();
        void Publish(string message, string routKey);
        void Received(string routKey);
        void Disconnection();
    }
}
