using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Web;
using System.Xml.Serialization;


namespace Core.Model.Tag
{
    [XmlRoot("analogInput")]
    public class AnalogInput : Tag
    {
        public AnalogInput()
        {
        }

        public AnalogInput(string tagName,string tagDesc,string ioAddress,double scanTime,string driver, List<Alarm> alarms, bool isOn, double lowLimit, double highLimit, string units):base(tagName,tagDesc,ioAddress)
        {
            ScanTime = scanTime;
            Alarms = alarms;
            IsOn = isOn;
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Driver = driver;
            Units = units;
        }
        

        [XmlAttribute("ScanTime")]
        public double ScanTime { get; set; }

        [XmlAttribute("LowLimit")]
        public double LowLimit { get; set; }

        [XmlAttribute("HighLimit")]
        public double HighLimit { get; set; }

        [XmlAttribute("IsOn")]
        public bool IsOn { get; set; }

        [XmlAttribute("driver")]
        public string Driver { get; set; }

        [XmlAttribute("Units")]
        public string Units { get; set; }

        [XmlElement("Alarm")]
        public List<Alarm> Alarms { get; set; } = new List<Alarm>();
        public void addAlarm(Alarm alarm)
        {
            Alarms.Add(alarm);
        }
    }
        
}
