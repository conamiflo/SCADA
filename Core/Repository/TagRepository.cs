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
        private string filename = "./tags.xml";
        public void AddAnalogInput(AnalogInput analogInput)
        {
            tags.Add(analogInput);

            XmlSerializer x = new XmlSerializer(typeof(Tag));
            TextWriter writer = new StreamWriter(filename);
            x.Serialize(writer, tags);

        }

        public bool DeleteAnalogInput(string id)
        {
            throw new NotImplementedException();
        }

        public AnalogInput GetAnalogInput(string id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {

            // return _userContext.Users.First(u => u.Id == id);
            return null;
        }

        public User GetUser(string username)

        {
            return null;
           // return _userContext.Users.FirstOrDefault(u => u.Username.Equals(username));
        }

        public AnalogInput UpdateAnalogInput(AnalogInput analogInput)
        {
            throw new NotImplementedException();
        }
    }
}