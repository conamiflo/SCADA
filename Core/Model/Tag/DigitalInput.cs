using System;
using System.Xml.Serialization;
using Core.Model; // Assuming Tag base class is defined here

namespace Core.Model.Tag
{
    [XmlRoot("digitalInput")]
    public class DigitalInput : Tag
    {
        public DigitalInput() { }

        public DigitalInput(string tagName, string tagDesc, string ioAddress, string driver, double scanTime, bool isOn)
            : base(tagName, tagDesc, ioAddress)
        {
            Driver = driver;
            ScanTime = scanTime;
            IsOn = isOn;
        }

        [XmlAttribute("ScanTime")]
        public double ScanTime { get; set; }

        [XmlAttribute("IsOn")]
        public bool IsOn { get; set; }

        [XmlAttribute("Driver")]
        public string Driver { get; set; }
    }
}
