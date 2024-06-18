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

        [Index(IsUnique = true)]
        [StringLength(100)]
        public string Username { get; set; }

        public string EncryptedPassword { get; set; }
        public User() { }
        public User(string username, string encryptedPassword)
        {
            Username = username;
            EncryptedPassword = encryptedPassword;
        }
    }
}