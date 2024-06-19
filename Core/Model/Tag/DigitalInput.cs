using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Model.Tag
{
    public class DigitalInput : Tag
    {
        public DigitalInput(string tagName, string description, string iOAddress,string driver, double scanTime, bool isOn) : base(tagName, description, iOAddress)
        {
            ScanTime = scanTime;
            IsOn = isOn;   
        }

        public double ScanTime { get; set; }
        public bool IsOn { get; set; }
    }


}