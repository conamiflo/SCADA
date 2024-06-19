using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.Model
{
    public class AlarmTrigger
    {
        [Key]
        public int Id { get; set; }
        public string TagName { get; set; }
        public DateTime Timestamp { get; set; }
        public double Threshold { get; set; }
        public AlarmType Type { get; set; }
        public int Priority { get; set; }
        public string Unit { get; set; }
        public double TagValue { get; set; }
    }
}