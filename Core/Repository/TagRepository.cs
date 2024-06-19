using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Context;
using Core.Repository.IRepository;
using Core.Model;
using Core.Model.Tag;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;

namespace Core.Repository
{
    public class TagRepository : ITagRepository
    {

        private List<Tag> tags= new List<Tag>();
        private string filename = HttpContext.Current.Server.MapPath("~/AppData/tags.xml");

        public TagRepository()
        {
            LoadTagsFromFile();
        }
        
        public void AddAnalogInput(AnalogInput analogInput)
        {
            tags.Add(analogInput);

            SaveTagsToFile();

        }

        public bool DeleteAnalogInput(string tagName)
        {
            Tag analogInputToRemove = tags.FirstOrDefault(tag => tag.TagName == tagName);

            if (analogInputToRemove != null)
            {
                tags.Remove(analogInputToRemove);
                SaveTagsToFile();
                return true;
            }

            return false;
        }

        public AnalogInput GetAnalogInput(string tagName)
        {
            return tags.FirstOrDefault(tag => tag.TagName == tagName) as AnalogInput;
        }


        public AnalogInput UpdateAnalogInput(AnalogInput analogInput)
        {
            AnalogInput existingAnalogInput = (AnalogInput) tags.FirstOrDefault(tag => tag.TagName == analogInput.TagName);

            if (existingAnalogInput != null)
            {
                existingAnalogInput.Description = analogInput.Description;
                existingAnalogInput.IOAddress = analogInput.IOAddress;
                existingAnalogInput.ScanTime = analogInput.ScanTime;
                existingAnalogInput.Alarms = analogInput.Alarms;
                existingAnalogInput.IsOn = analogInput.IsOn;
                existingAnalogInput.LowLimit = analogInput.LowLimit;
                existingAnalogInput.HighLimit = analogInput.HighLimit;
                existingAnalogInput.Units = analogInput.Units;

                SaveTagsToFile();

                return existingAnalogInput;
            }

            return null;
        }


        private void LoadTagsFromFile()
        {
            if (File.Exists(filename))
            {
                try
                {
                    XmlSerializer x = new XmlSerializer(typeof(List<Tag>), new XmlRootAttribute("ArrayOfTag"));
                    using (FileStream fs = new FileStream(filename, FileMode.Open))
                    {
                        tags = (List<Tag>)x.Deserialize(fs);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading tags from XML file: " + ex.Message);
                }
            }
        }
        private void SaveTagsToFile()
        {
            try
            {
                XmlSerializer x = new XmlSerializer(typeof(List<Tag>), new XmlRootAttribute("ArrayOfTag"));
                using (TextWriter writer = new StreamWriter(filename))
                {
                    x.Serialize(writer, tags);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving tags to XML file: " + ex.Message);
            }
        }
    }
}