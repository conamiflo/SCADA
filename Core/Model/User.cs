using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Core.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public User() { }
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}