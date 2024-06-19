using Core.Context;
using Core.Model.Tag;
using Core.Repository;
using Core.Repository.IRepository;
using Core.Service;
using Core.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Core
{
    public class CoreService : IUserService, ITagService
    {

        private IUserService userService;
        private ITagService tagService;

        public CoreService()
        {
            userService = new UserService(new UserRepository());
            tagService = new TagService(new TagRepository());
        }



        public string Login(string username, string password)
        {
            return userService.Login(username, password);
        }

        public bool Logout(string token)
        {
            return userService.Logout(token);
        }

        public string Registration(string username, string password)
        {
            return userService.Registration(username, password);
        }
        public void AddAnalogInput(AnalogInput analogInput)
        {
            tagService.AddAnalogInput(analogInput);
        }

        public bool DeleteAnalogInput(string id)
        {
            return tagService.DeleteAnalogInput(id);
        }

        public AnalogInput GetAnalogInput(string id)
        {
            return tagService.GetAnalogInput(id);
        }
        public AnalogInput UpdateAnalogInput(AnalogInput analogInput)
        {
            return tagService.UpdateAnalogInput(analogInput);
        }

        public List<AnalogInput> GetAllAnalogInputs()
        {
            return tagService.GetAllAnalogInputs();
        }
        public void AddAnalogOutput(AnalogOutput analogOutput)
        {
            tagService.AddAnalogOutput(analogOutput);
        }

        public bool DeleteAnalogOutput(string id)
        {
            return tagService.DeleteAnalogOutput(id);
        }

        public AnalogOutput GetAnalogOutput(string id)
        {
            return tagService.GetAnalogOutput(id);
        }

        public AnalogOutput UpdateAnalogOutput(AnalogOutput analogOutput)
        {
            return tagService.UpdateAnalogOutput(analogOutput);
        }

        public List<AnalogOutput> GetAllAnalogOutputs()
        {
            return tagService.GetAllAnalogOutputs();
        }

        public void AddDigitalInput(DigitalInput digitalInput)
        {
            tagService.AddDigitalInput(digitalInput);
        }

        public bool DeleteDigitalInput(string id)
        {
            return tagService.DeleteDigitalInput(id);
        }

        public DigitalInput GetDigitalInput(string id)
        {
            return tagService.GetDigitalInput(id);
        }

        public DigitalInput UpdateDigitalInput(DigitalInput digitalInput)
        {
            return tagService.UpdateDigitalInput(digitalInput);
        }

        public List<DigitalInput> GetAllDigitalInputs()
        {
            return tagService.GetAllDigitalInputs();
        }

        public void AddDigitalOutput(DigitalOutput digitalOutput)
        {
            tagService.AddDigitalOutput(digitalOutput);
        }

        public bool DeleteDigitalOutput(string id)
        {
            return tagService.DeleteDigitalOutput(id);
        }   

        public DigitalOutput GetDigitalOutput(string id)
        {
            return tagService.GetDigitalOutput(id);
        }

        public DigitalOutput UpdateDigitalOutput(DigitalOutput digitalOutput)
        {
            return tagService.UpdateDigitalOutput(digitalOutput);
        }

        public List<DigitalOutput> GetAllDigitalOutputs()
        {
            return tagService.GetAllDigitalOutputs();
        }
    }
}
