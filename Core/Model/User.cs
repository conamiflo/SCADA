using System;
using Microsoft.AspNetCore.Identity;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Password { get; set; }
        public User() { }
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}