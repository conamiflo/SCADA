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
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<AnalogInput> AnalogInput { get; set; }
        public DbSet<AnalogOutput> AnalogOutput { get; set; }
        public DbSet<DigitalOutput> DigitalOutput { get; set; }
        public DbSet<DigitalInput> DigitalInput { get; set; }
        /*public DbSet<InputsValue> InputsValues { get; set; }
        public DbSet<OutputsValue> OutputsValues { get; set; }
        public DbSet<AlarmValue> AlarmsValue { get; set; }*/


        public SCADAContext() : base("name=SCADAContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alarm>()
            .HasRequired(a => a.AnalogInput)
            .WithMany(t => t.Alarms)
            .HasForeignKey(a => a.AnalogInputId);

            modelBuilder.Entity<AnalogOutput>().HasKey(t => t.TagName);
            modelBuilder.Entity<AnalogInput>().HasKey(t => t.TagName);
            modelBuilder.Entity<DigitalOutput>().HasKey(t => t.TagName);
            modelBuilder.Entity<DigitalInput>().HasKey(t => t.TagName);

            base.OnModelCreating(modelBuilder);
        }
    }
}