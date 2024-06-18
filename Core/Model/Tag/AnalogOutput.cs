using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Model.Tag
{
    public class AnalogOutput : Tag
    {
        public AnalogOutput(string tagName, string tagDesc, string ioAddress,double initialValue, double lowLimit, double highLimit, string units) : base(tagName, tagDesc, ioAddress)
        {
            InitialValue = initialValue;
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Units = units;
        }

        public double InitialValue { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Units { get; set; }
    }
}