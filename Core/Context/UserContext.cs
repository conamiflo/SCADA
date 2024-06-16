using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace Core.Context
{
    public class UserContext : DbContext
    {
        public UserContext() : base("name=UserContext")
        {

        }
        public DbSet<User> Users { get; set; }
    }
}