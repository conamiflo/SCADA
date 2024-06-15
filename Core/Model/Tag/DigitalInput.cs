using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Model.Tag
{
    public class DigitalInput : Tag
    {
        public double ScanTime { get; set; }
        public bool IsOn { get; set; }
    }
}