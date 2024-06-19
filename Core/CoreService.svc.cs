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
    }
}
