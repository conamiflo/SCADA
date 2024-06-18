﻿using System;
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
        public string TagName { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
    }
}