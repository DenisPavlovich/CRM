using CRM.Data.Dto;

namespace CRM.Model.Interfaces.Service
{
    public interface IServiceUseCase
    {
        void AddOrganization(Dto obj);
        void AddClient(Dto obj);
        void EditClient(int id, string address = null, string discription = null, string email = null, string name = null);
        void AppendPhoneNumberToClient(int id, string number);
        void TakeManagerOwnerToClient(int id, Manager manager);
    }
}
