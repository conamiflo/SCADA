using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Core.Model.Tag
{
    [XmlInclude(typeof(AnalogInput))]
    public abstract class Tag
    {
        [Key]
        public string TagName { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
    }
}