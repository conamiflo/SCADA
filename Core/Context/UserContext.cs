using Core.Model;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Context
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}