using Core.Model;
using Core.Repository.IRepository;
using Core.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public User GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }

        public User Login(string username, string password)
        {
            User user = _userRepository.GetUser(username);
            if(user != null)
            {
                if (password.Equals(user.Password))
                {
                    return user;
                }
            }
            return null;
        }

        public User Register(string username, string password)
        {
            User user = _userRepository.GetUser(username);
            if (user != null)
            {
                throw new Exception("User with this username already exists");
            }
            return _userRepository.AddUser(username, password);
        }
    }
}