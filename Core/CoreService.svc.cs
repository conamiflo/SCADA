using Core.Context;
using Core.Model;
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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CoreService : IUserService, ITagService, IAlarmService
    {

        public Dictionary<string, IAlarmCallback> CallbackChannels = new Dictionary<string, IAlarmCallback>();

        public IUserService userService;
        public ITagService tagService;
        public IAlarmService alarmService;

        public CoreService()
        {
            userService = new UserService(new UserRepository());
            tagService = new TagService(new TagRepository());
            alarmService = new AlarmService(new AlarmRepository());
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

        public void AddAlarm(AlarmTrigger alarm)
        {
            alarmService.AddAlarm(alarm);
        }
        public void RemoveAlarm(AlarmTrigger alarm)
        {
            alarmService.RemoveAlarm(alarm);
        }

        public AlarmTrigger GetAlarmById(int id)
        {
            return alarmService.GetAlarmById(id);
        }

        public IEnumerable<AlarmTrigger> GetAllAlarms()
        {
            return alarmService.GetAllAlarms();
        }

        public IEnumerable<AlarmTrigger> GetAlarmsInPeriod(DateTime startTime, DateTime endTime, Core.Service.AlarmService.SortOption sortOption)
        {
            return alarmService.GetAlarmsInPeriod(startTime, endTime, sortOption);
        }

        public IEnumerable<AlarmTrigger> GetAlarmsByPriority(int priority)
        {
            return alarmService.GetAlarmsByPriority(priority);
        }

        public void LogAlarm(AlarmTrigger alarm)
        {
            alarmService.LogAlarm(alarm);
        }
    }
}
