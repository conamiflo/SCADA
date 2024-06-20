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
    public class CoreService : IUserService, ITagService, IAlarmService, ITrendingService
    {

        public List<IAlarmCallback> alarmCallbacks = new List<IAlarmCallback>();
        public static List<ITrendingCallback> trendingCallbacks = new List<ITrendingCallback>();


        public IUserService userService;
        public ITagService tagService;
        public IAlarmService alarmService;





        public CoreService()
        {
            userService = new UserService(new UserRepository());
            tagService = new TagService(new TagRepository());
            alarmService = new AlarmService(new AlarmRepository());
            new TagProcessing(tagService,this);

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

        public bool DeleteTag(string id)
        {
            return tagService.DeleteTag(id);
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

            foreach (var callback in alarmCallbacks)
            {
                callback.AlarmTriggered($"Alarm Triggered: Id={alarm.Id}, Type={alarm.Type}, Priority={alarm.Priority}, Threshold={alarm.Threshold}, Timestamp={DateTime.Now}");
            }
        }

        public void SubscribeToTrending()
        {
            var a = OperationContext.Current.GetCallbackChannel<ITrendingCallback>();
            trendingCallbacks.Add(a);

            Dictionary<string, double> tags = new Dictionary<string, double>();

            var analInput = tagService.GetAllAnalogInputs();
            var analOutput = tagService.GetAllAnalogOutputs();
            var digInput = tagService.GetAllDigitalInputs();
            var digOutput = tagService.GetAllDigitalOutputs();

            foreach (var item in analInput)
            {
                tags.Add(item.TagName, 0);
            }
            foreach (var item in analOutput)
            {
                tags.Add(item.TagName, 0);
            }
            foreach (var item in digInput)
            {
                tags.Add(item.TagName, 0);
            }
            foreach (var item in digOutput)
            {
                tags.Add(item.TagName, 0);
            }

            List<ITrendingCallback> activeCallbacks = new List<ITrendingCallback>();
            foreach (var callback in trendingCallbacks)
            {
                try
                {
                    callback.initTagTable(tags);
                    activeCallbacks.Add(callback);
                }
                catch (Exception e)
                {

                }

            }
            trendingCallbacks = activeCallbacks;
        }

        public void addTagValue(string tagName, double value)
        {
            List<ITrendingCallback> activeCallbacks = new List<ITrendingCallback>();
            foreach (var callback in trendingCallbacks)
            {
                try
                {
                    callback.addTagValue(tagName, value);
                    activeCallbacks.Add(callback);

                }
                catch (Exception e)
                {
                }
            }
            trendingCallbacks = activeCallbacks;

        }

        public void removeTag(string tagName)
        {
            List<ITrendingCallback> activeCallbacks = new List<ITrendingCallback>();
            foreach (var callback in trendingCallbacks)
            {
                try
                {
                    callback.removeTag(tagName);
                    activeCallbacks.Add(callback);

                }
                catch (Exception e)
                {
                }
            }
            trendingCallbacks = activeCallbacks;
        }

        public void ToggleTagScan(string inputTag, bool isOn,bool isAnalog)
        {
            tagService.ToggleTagScan(inputTag, isOn,isAnalog);
        }
    }
}
