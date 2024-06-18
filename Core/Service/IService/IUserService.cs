using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.IService
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        string Registration(string username, string password);

        [OperationContract]
        string Login(string username, string password);

        [OperationContract]
        bool Logout(string token);
    }
}
