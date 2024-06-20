using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace Core.Model
{
    [XmlRoot("RTUAdress")]
    public class RTUAdress
    {
        public RTUAdress()
        {
        }

        public RTUAdress(string adress)
        {
            Adress = adress;
        }

        [XmlAttribute("Adress")]
        public string Adress { get; set; }
    }
}