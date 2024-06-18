using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Web;
using Core.Model;
using Core.Model.Alarm;


namespace Core.Model.Tag
{
    public class AnalogInput : Tag
    {
        public AnalogInput()
        {
        }

        public AnalogInput(string tagName,string tagDesc,string ioAddress,double scanTime, List<Model.Alarm.Alarm> alarms, bool isOn, double lowLimit, double highLimit, string units):base(tagName,tagDesc,ioAddress)
        {
            ScanTime = scanTime;
            Alarms = alarms;
            IsOn = isOn;
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Units = units;
        }
        

        public double ScanTime { get; set; }
        public List<Alarm.Alarm> Alarms { get; set; } = new List<Alarm.Alarm>();
        public bool IsOn { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Units { get; set; }
    }
        
}
