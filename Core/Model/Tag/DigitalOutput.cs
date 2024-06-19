using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Model.Tag
{
    public class DigitalOutput : Tag
    {
        public DigitalOutput(string tagName, string description, string iOAddress,int nitialValue) : base(tagName, description, iOAddress)
        {
            InitialValue = nitialValue;
        }

        public int InitialValue { get; set; }
    }
}