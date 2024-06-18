using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.Model.Tag
{
    public enum ValueType
    {
        ANALOG, DIGITAL
    }

    public class InputsValue
    {
        [Key]
        public int Id { get; set; }
        public string IOAddress { get; set; }
        public string TagName { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Value { get; set; }
        public ValueType ValueType { get; set; }

        public InputsValue() { }

        public InputsValue(string address, string tagName, double value, ValueType valueType)
        {
            IOAddress = address;
            TagName = tagName;
            TimeStamp = DateTime.Now;
            Value = value;
            ValueType = valueType;
        }
    }
}