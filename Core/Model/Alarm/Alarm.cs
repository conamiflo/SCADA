using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace Core.Model
{
    public enum AlarmType
    {
        LOW, HIGH
    }
    [XmlRoot("alarm")]
    public class Alarm
    {
        [Key]
        [XmlIgnore]
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

        [XmlAttribute("Threshold")]
        public double Threshold { get; set; }

        [XmlAttribute("Type")]
        public AlarmType Type { get; set; }

        [XmlAttribute("Priority")]
        public int Priority { get; set; }

        [XmlAttribute("Unit")]
        public string Unit { get; set; }

        public Alarm() { }
        public Alarm(int id, double threshold, AlarmType type, int priority, string unit)
        {
            Id = id;
            Threshold = threshold;
            Type = type;
            Priority = priority;
            Unit = unit;
        }
    }
}
