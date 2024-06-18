using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.Model.Tag
{
    public abstract class Tag
    {
        [Key]
        public string TagName { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
    }
}