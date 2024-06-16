using Core.Context;
using Core.Model;
using Core.Repository.IRepository;
using Core.Service.IService;
using Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Core.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private static Dictionary<string, User> authenticatedUsers =new Dictionary<string, User>();
        private RNGCryptoServiceProvider crypto;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            crypto = new RNGCryptoServiceProvider();
        }

        public string Login(string username, string password)
        {
            User user = _userRepository.GetUser(username);
            if(user != null)
            {
                if (EncryptUtil.ValidateEncryptedData(password, user.EncryptedPassword))
                {
                    string token = GenerateToken(username);
                    authenticatedUsers.Add(token, user);
                    return token;

                }
            }
            return "Login failed";
        }

        public bool Logout(string token)
        {
            return authenticatedUsers.Remove(token);
        }

        public bool Registration(string username, string password)
        {
            string encryptedPassword = EncryptUtil.EncryptData(password);
            User user = _userRepository.AddUser(username, encryptedPassword);
            if(user == null) { return  false; }
            return true;
        }

        private bool IsUserAuthenticated(string token)
        {
            return authenticatedUsers.ContainsKey(token);
        }

        private string GenerateToken(string username)
        {
            byte[] randVal = new byte[32];
            crypto.GetBytes(randVal);
            string randStr = Convert.ToBase64String(randVal);
            return username + randStr;
        }

    }
}