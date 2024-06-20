using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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