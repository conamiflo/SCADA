using Core.Model.Tag;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Core.Model.Alarm
{
    public enum AlarmType
    {
        LOW, HIGH
    }

    public class Alarm
    {

        [Key]
        public int Id { get; set; }
        public double Threshold { get; set; }
        public AlarmType Type { get; set; }
        public int Priority { get; set; }
        public int AnalogInputId { get; set; }
    }
}