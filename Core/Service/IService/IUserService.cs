using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.IService
{
    public interface IUserService
    {
        [OperationContract]
        bool Registration(string username, string password);

        [OperationContract]
        string Login(string username, string password);

        [OperationContract]
        bool Logout(string token);
    }
}
