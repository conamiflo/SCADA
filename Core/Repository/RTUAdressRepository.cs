using Core.Model;
using Core.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Core.Repository
{
    public class RTUAdressRepository : IRTUAdressRepository
    {
        private List<RTUAdress> rtuAddresses = new List<RTUAdress>();
        private string filename = HttpContext.Current.Server.MapPath("~/AppData/rtuAddresses.xml");

        public RTUAdressRepository()
        {
            LoadRTUAddressesFromFile();
        }

        public void AddRTUAdress(RTUAdress address)
        {
            rtuAddresses.Add(address);
            SaveRTUAddressesToFile();
        }

        public bool DeleteRTUAdress(string value)
        {
            RTUAdress addressToRemove = rtuAddresses.FirstOrDefault(address => address.Adress == value);
            if (addressToRemove != null)
            {
                rtuAddresses.Remove(addressToRemove);
                SaveRTUAddressesToFile();
                return true;
            }
            return false;
        }

        public RTUAdress GetRTUAdress(string value)
        {
            return rtuAddresses.FirstOrDefault(address => address.Adress == value);
        }

        public List<RTUAdress> GetRTUAdresses()
        {
            return rtuAddresses;
        }

        public RTUAdress UpdateRTUAdress(RTUAdress address)
        {
            RTUAdress existingAddress = rtuAddresses.FirstOrDefault(addr => addr.Adress == address.Adress);
            if (existingAddress != null)
            {
                existingAddress.Adress = address.Adress;
                SaveRTUAddressesToFile();
                return existingAddress;
            }
            return null;
        }

        private void LoadRTUAddressesFromFile()
        {
            if (File.Exists(filename))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<RTUAdress>), new XmlRootAttribute("ArrayOfRTUAdress"));
                    using (FileStream fs = new FileStream(filename, FileMode.Open))
                    {
                        rtuAddresses = (List<RTUAdress>)serializer.Deserialize(fs);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading RTU addresses from XML file: " + ex.Message);
                }
            }
        }

        private void SaveRTUAddressesToFile()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<RTUAdress>), new XmlRootAttribute("ArrayOfRTUAdress"));
                using (TextWriter writer = new StreamWriter(filename))
                {
                    serializer.Serialize(writer, rtuAddresses);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving RTU addresses to XML file: " + ex.Message);
            }
        }
    }
}