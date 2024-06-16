using Core.Context;
using Core.Repository;
using Core.Repository.IRepository;
using Core.Service;
using Core.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Core
{
    public class CoreService : IUserService
    {

        private IUserService userService;

        public CoreService()
        {
            IUserRepository userRepository = new UserRepository();
            userService = new UserService(userRepository);
        }

        public string Login(string username, string password)
        {
            string message = "Login failed";

            try
            {
                return userService.Login(username, password);
            }
            catch (Exception ex)
            {
                message = $"{ex.Message}";
            }
            return message;
        }

        public bool Logout(string token)
        {
            return userService.Logout(token);
        }

        public string Registration(string username, string password)
        {
            string message = "Registration failed";
            try
            {
                return userService.Registration(username, password);
            }
            catch (ArgumentException ex)
            {
                message = $"Registration failed: {ex.Message}";
            }
            catch (Exception ex)
            {
                message = $"{ex.Message}";
            }
            return message;
        }
    }
}
