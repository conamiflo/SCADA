using Core.Model.Tag;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Reflection.Emit;
namespace Core.Context
{
    public class SCADAContext : DbContext
    {
        public DbSet<AlarmTrigger> AlarmTriggers { get; set; }
        public DbSet<InputsValue> InputsValues { get; set; }
        public DbSet<OutputsValue> OutputsValues { get; set; }



        public SCADAContext() : base("name=SCADAContext")
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<InputsValue>().HasKey(input => input.Id);
        //    modelBuilder.Entity<OutputsValue>().HasKey(output => output.Id);

        //    modelBuilder.Entity<AlarmTrigger>()
        //    .HasRequired<string>(a => a.TagName)
        //    .WithMany()
        //    .HasForeignKey(a => a.TagName);

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}