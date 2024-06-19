using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Core.Model.Tag
{
    [XmlInclude(typeof(AnalogInput))]
    public abstract class Tag
    {
        public Tag()
        {
        }

        public Tag(string tagName, string description, string iOAddress)
        {
            TagName = tagName;
            Description = description;
            IOAddress = iOAddress;
        }

        [Key]
        [XmlAttribute("TagName")]
        public string TagName { get; set; }
        [XmlAttribute("Description")]
        public string Description { get; set; }
        [XmlAttribute("IOAddress")]
        public string IOAddress { get; set; }
    }
}