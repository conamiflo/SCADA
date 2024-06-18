using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.Model.Tag
{
    public class OutputsValue
    {
        [Key]
        public int Id { get; set; }
        public string IOAddress { get; set; }
        public string TagName { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Value { get; set; }
        public ValueType ValueType { get; set; }

        public OutputsValue() { }
        public OutputsValue(string iOAddress, string tagName, double value, ValueType valueType)
        {
            IOAddress = iOAddress;
            TagName = tagName;
            TimeStamp = DateTime.Now;
            Value = value;
            ValueType = valueType;
        }
    }
}