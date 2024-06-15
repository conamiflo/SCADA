using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Model.Tag
{
    public abstract class Tag
    {
        string TagName { get; set; }
        string Description { get; set; }
        string IOAddress { get; set; }
    }
}