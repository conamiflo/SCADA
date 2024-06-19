using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Core.Model.Tag
{
    [XmlRoot("digitalOutput")]
    public class DigitalOutput : Tag
    {
        public DigitalOutput() { }

        public DigitalOutput(string tagName, string description, string ioAddress, int initialValue)
            : base(tagName, description, ioAddress)
        {
            InitialValue = initialValue;
        }

        [XmlAttribute("InitialValue")]
        public int InitialValue { get; set; }
    }
}