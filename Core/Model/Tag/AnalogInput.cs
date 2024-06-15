using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;


namespace Core.Model.Tag
{
    public class AnalogInput : Tag
    {
        public double ScanTime { get; set; }
        public List<Alarm> Alarms { get; set; } = new List<Alarm>();
        public bool IsOn { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Units { get; set; }
    }
        
}
