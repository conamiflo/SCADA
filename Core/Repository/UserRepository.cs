﻿using Core.Context;
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
        private readonly UserContext _userContext;

        public UserRepository(UserContext context)
        {
            _userContext = context;
        }

        public User AddUser(string email, string password)
        {
            User newUser = new User(email,password);
            var entityEntry = _userContext.Users.Add(newUser);
            _userContext.SaveChanges();
            return entityEntry;
        }

        public User GetUser(int id)
        {
            return _userContext.Users.First(u => u.Id == id);
        }

        public User GetUser(string email)
        {
            return _userContext.Users.FirstOrDefault(u => u.Username.Equals(email));
        }
    }
}