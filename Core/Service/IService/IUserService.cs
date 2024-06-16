using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.IService
{
    public interface IUserService
    {
        User GetUser(int id);
        User Register(string username, string password);
        User Login(string username, string password);
    }
}
