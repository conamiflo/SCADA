using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

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
        public int Id { get; set; }

        [XmlAttribute("Threshold")]
        public double Threshold { get; set; }

        [XmlAttribute("Type")]
        public AlarmType Type { get; set; }

        [XmlAttribute("Priority")]
        public int Priority { get; set; }

        [XmlAttribute("Unit")]
        public string Unit { get; set; }

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
