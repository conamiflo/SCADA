using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.Model
{
    public class AlarmTrigger
    {
        [Key]
        public int Id { get; set; }
        public string TagName { get; set; }
        public DateTime Timestamp { get; set; }
        public double Threshold { get; set; }
        public AlarmType Type { get; set; }
        public int Priority { get; set; }
        public string Unit { get; set; }
        public double TagValue { get; set; }

        public AlarmTrigger()
        {
        }

        public AlarmTrigger(Alarm a,string tagName,DateTime timeStamp,double value)
        {
            Id= DateTime.Now.GetHashCode();
            TagName = tagName;
            Timestamp = timeStamp;
            Threshold = a.Threshold;
            Type = a.Type;
            Priority = a.Priority;
            Unit = a.Unit;
            TagValue = value;
        }

        public AlarmTrigger(int id, string tagName, DateTime timestamp, double threshold, AlarmType type, int priority, string unit, double tagValue)
        {
            Id = id;
            TagName = tagName;
            Timestamp = timestamp;
            Threshold = threshold;
            Type = type;
            Priority = priority;
            Unit = unit;
            TagValue = tagValue;
        }

        public AlarmTrigger(int id, string tagName, DateTime timestamp, double tagValue, Alarm alarm)
        {
            Id = id;
            TagName = tagName;
            Timestamp = timestamp;
            Threshold = alarm.Threshold;
            Type = alarm.Type;
            Priority = alarm.Priority;
            Unit = alarm.Unit;
            TagValue = tagValue;
        }

        public override string ToString()
        {
            return $"Alarm Triggered: Id={Id}, Type={Type}, Priority={Priority}, Threshold={Threshold}, Timestamp={Timestamp}";
        }
    }
}