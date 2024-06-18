using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.IRepository
{
    public interface IUserRepository
    {
        User GetUser(int id);
        User GetUser(string username);
        User AddUser(string username, string password);
    }
}
