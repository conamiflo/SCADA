using Core.Context;
using Core.Model;
using Core.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Web;

namespace Core.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserContext _userContext;

        public UserRepository()
        {
            _userContext = new UserContext();
        }
                
        public User AddUser(string username, string password)
        {
            if(GetUser(username) != null)
            {
                return null;
            }
            User newUser = new User(username, password);
            
            var entityEntry = _userContext.Users.Add(newUser);
            _userContext.SaveChanges();
            return entityEntry;
        }

        public User GetUser(int id)
        {
            
            return _userContext.Users.First(u => u.Id == id);
        }

        public User GetUser(string username)
        {
            
            return _userContext.Users.FirstOrDefault(u => u.Username.Equals(username));
        }
    }
}