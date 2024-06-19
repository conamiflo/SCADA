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
        public List<AnalogInput> GetAllAnalogInputs()
        {
            return tags.OfType<AnalogInput>().ToList();
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
                existingAnalogInput.Driver = analogInput.Driver;

                SaveTagsToFile();

                return existingAnalogInput;
            }

            return null;
        }

        public List<AnalogOutput> GetAllAnalogOutputs()
        {
            return tags.OfType<AnalogOutput>().ToList();
        }

        public AnalogOutput GetAnalogOutput(string tagName)
        {
            return tags.FirstOrDefault(tag => tag.TagName == tagName) as AnalogOutput;
        }

        public void AddAnalogOutput(AnalogOutput analogOutput)
        {
            tags.Add(analogOutput);
            SaveTagsToFile();
        }

        public bool DeleteAnalogOutput(string tagName)
        {
            Tag analogOutputToRemove = tags.FirstOrDefault(tag => tag.TagName == tagName);

            if (analogOutputToRemove != null && analogOutputToRemove is AnalogOutput)
            {
                tags.Remove(analogOutputToRemove);
                SaveTagsToFile();
                return true;
            }

            return false;
        }

        public AnalogOutput UpdateAnalogOutput(AnalogOutput analogOutput)
        {
            AnalogOutput existingAnalogOutput = tags.FirstOrDefault(tag => tag.TagName == analogOutput.TagName) as AnalogOutput;

            if (existingAnalogOutput != null)
            {
                existingAnalogOutput.Description = analogOutput.Description;
                existingAnalogOutput.IOAddress = analogOutput.IOAddress;
                existingAnalogOutput.InitialValue = analogOutput.InitialValue;
                existingAnalogOutput.LowLimit = analogOutput.LowLimit;
                existingAnalogOutput.HighLimit = analogOutput.HighLimit;
                existingAnalogOutput.Units = analogOutput.Units;

                SaveTagsToFile();

                return existingAnalogOutput;
            }

            return null;
        }

        public List<DigitalInput> GetAllDigitalInputs()
        {
            return tags.OfType<DigitalInput>().ToList();
        }

        public DigitalInput GetDigitalInput(string tagName)
        {
            return tags.FirstOrDefault(tag => tag.TagName == tagName) as DigitalInput;
        }

        public void AddDigitalInput(DigitalInput digitalInput)
        {
            tags.Add(digitalInput);
            SaveTagsToFile();
        }

        public bool DeleteDigitalInput(string tagName)
        {
            Tag digitalInputToRemove = tags.FirstOrDefault(tag => tag.TagName == tagName);

            if (digitalInputToRemove != null && digitalInputToRemove is DigitalInput)
            {
                tags.Remove(digitalInputToRemove);
                SaveTagsToFile();
                return true;
            }

            return false;
        }

        public DigitalInput UpdateDigitalInput(DigitalInput digitalInput)
        {
            DigitalInput existingDigitalInput = tags.FirstOrDefault(tag => tag.TagName == digitalInput.TagName) as DigitalInput;

            if (existingDigitalInput != null)
            {
                existingDigitalInput.Description = digitalInput.Description;
                existingDigitalInput.IOAddress = digitalInput.IOAddress;
                existingDigitalInput.ScanTime = digitalInput.ScanTime;
                existingDigitalInput.IsOn = digitalInput.IsOn;
                existingDigitalInput.Driver = digitalInput.Driver;

                SaveTagsToFile();

                return existingDigitalInput;
            }

            return null;
        }

        public List<DigitalOutput> GetAllDigitalOutputs()
        {
            return tags.OfType<DigitalOutput>().ToList();
        }

        public DigitalOutput GetDigitalOutput(string tagName)
        {
            return tags.FirstOrDefault(tag => tag.TagName == tagName) as DigitalOutput;
        }

        public void AddDigitalOutput(DigitalOutput digitalOutput)
        {
            tags.Add(digitalOutput);
            SaveTagsToFile();
        }

        public bool DeleteDigitalOutput(string tagName)
        {
            Tag digitalOutputToRemove = tags.FirstOrDefault(tag => tag.TagName == tagName);

            if (digitalOutputToRemove != null && digitalOutputToRemove is DigitalOutput)
            {
                tags.Remove(digitalOutputToRemove);
                SaveTagsToFile();
                return true;
            }

            return false;
        }

        public DigitalOutput UpdateDigitalOutput(DigitalOutput digitalOutput)
        {
            DigitalOutput existingDigitalOutput = tags.FirstOrDefault(tag => tag.TagName == digitalOutput.TagName) as DigitalOutput;

            if (existingDigitalOutput != null)
            {
                existingDigitalOutput.Description = digitalOutput.Description;
                existingDigitalOutput.IOAddress = digitalOutput.IOAddress;
                existingDigitalOutput.InitialValue = digitalOutput.InitialValue;

                SaveTagsToFile();

                return existingDigitalOutput;
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