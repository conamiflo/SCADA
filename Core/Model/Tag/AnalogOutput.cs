using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Core.Model.Tag
{
    [XmlRoot("analogOutput")]
    public class AnalogOutput : Tag
    {
        public AnalogOutput() { }

        public AnalogOutput(string tagName, string tagDesc, string ioAddress, double initialValue, double lowLimit, double highLimit, string units)
            : base(tagName, tagDesc, ioAddress)
        {
            InitialValue = initialValue;
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Units = units;
        }

        [XmlAttribute("InitialValue")]
        public double InitialValue { get; set; }

        [XmlAttribute("LowLimit")]
        public double LowLimit { get; set; }

        [XmlAttribute("HighLimit")]
        public double HighLimit { get; set; }

        [XmlAttribute("Units")]
        public string Units { get; set; }
    }
}