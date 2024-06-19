using Core.Model.Tag;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace Core.Model.Alarm
{
    public enum AlarmType
    {
        LOW, HIGH
    }

    public class Alarm
    {
        public Alarm(double threshold, AlarmType type, int priority,string unit)
        {

            Id = DateTime.Now.GetHashCode();
            Threshold = threshold;
            Type = type;
            Priority = priority;
            Unit = unit;
        }

        [Key]
        public int Id { get; set; }
        public double Threshold { get; set; }
        public AlarmType Type { get; set; }
        public int Priority { get; set; }
        public string Unit { get; set; }

    }
}