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
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ValueType = Core.Model.Tag.ValueType;

namespace Core
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CoreService : IUserService, ITagService, IAlarmService, ITrendingService, ITagValueService, IRealTimeDriver, IRTUAdressService
    {

        public List<IAlarmCallback> alarmCallbacks = new List<IAlarmCallback>();
        public static List<ITrendingCallback> trendingCallbacks = new List<ITrendingCallback>();

        public IUserService userService;
        public ITagService tagService;
        public IAlarmService alarmService;
        public ITagValueService tagValueService;
        public IRTUAdressService rtuAdressService;
        public TagProcessing tagProcessing;
        public RealTimeDriver realTimeDriver;

        public CoreService()
        {
            realTimeDriver = new RealTimeDriver(new RTUAdressService(new RTUAdressRepository()));
            userService = new UserService(new UserRepository());
            tagService = new TagService(new TagRepository());
            alarmService = new AlarmService(new AlarmRepository());
            tagValueService = new TagValueService(new TagValueRepository());
            tagProcessing = new TagProcessing(tagService,this);
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
            tagProcessing.addAnalogTag(analogInput);

        }

        public bool DeleteTag(string id)
        {
            tagProcessing.deleteTag(id);
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
            tagValueService.AddOutputsValue(new OutputsValue(analogOutput.IOAddress, analogOutput.TagName, analogOutput.InitialValue, ValueType.ANALOG));

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
            tagProcessing.addDigitalTag(digitalInput);

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
            tagValueService.AddOutputsValue(new OutputsValue (digitalOutput.IOAddress,digitalOutput.TagName,digitalOutput.InitialValue,ValueType.DIGITAL));
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

        public IEnumerable<AlarmTrigger> GetAlarmsInPeriod(DateTime startTime, DateTime endTime, bool sortOption)
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
            List<IAlarmCallback> activeCallbacks = new List<IAlarmCallback>();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < alarm.Priority; i++)
            {
                sb.AppendLine($"Alarm Triggered: Id={alarm.Id},Tag={alarm.TagName},Tag value= {alarm.TagValue} ,Type={alarm.Type}, Priority={alarm.Priority}, Threshold={alarm.Threshold}, Timestamp={alarm.Timestamp}\n");
            }
            string message = sb.ToString();
            foreach (var callback in alarmCallbacks)
            {
                try
                {
                    callback.AlarmTriggered(message);
                    activeCallbacks.Add(callback);

                }
                catch (Exception e)
                {
                }
            }
            alarmCallbacks = activeCallbacks;
        }

        public InputsValue GetInputsValue(int id)
        {
            return tagValueService.GetInputsValue(id);
        }

        public OutputsValue GetOutputsValue(int id)
        {
            return tagValueService.GetOutputsValue(id);
        }

        public List<InputsValue> GetAllInputsValues()
        {
            return tagValueService.GetAllInputsValues();
        }

        public List<OutputsValue> GetAllOutputsValues()
        {
            return tagValueService.GetAllOutputsValues();
        }

        public void AddInputsValue(InputsValue inputsValue)
        {
            tagValueService.AddInputsValue(inputsValue);
        }

        public void AddOutputsValue(OutputsValue outputsValue)
        {
           tagValueService.AddOutputsValue(outputsValue);
           addTagValue(outputsValue.TagName, outputsValue.Value);
        }

        public bool RemoveInputsValue(int id)
        {
            return tagValueService.RemoveInputsValue(id);
        }

        public bool RemoveOutputValue(int id)
        {
            return tagValueService.RemoveOutputValue(id);
        }

        public InputsValue UpdateInputsValue(InputsValue inputsValue)
        {
            return tagValueService.UpdateInputsValue(inputsValue);
        }

        public OutputsValue UpdateOutputsValue(OutputsValue outputsValue)
        {
            return tagValueService.UpdateOutputsValue(outputsValue);
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
            tagService.ToggleTagScan(inputTag, isOn, isAnalog);

            if (isAnalog)
            {
                AnalogInput analogInput = GetAnalogInput(inputTag);
                analogInput.IsOn = isOn;

                if (isOn)
                {
                    tagProcessing.addAnalogTag(analogInput);
                }
            }
            else
            {
                DigitalInput digitalInput = GetDigitalInput(inputTag);
                digitalInput.IsOn = isOn;

                if (isOn)
                {
                    tagProcessing.addDigitalTag(digitalInput);
                }
            }
        }

        public void SubscribeToAlarmDisplay()
        {
            var a = OperationContext.Current.GetCallbackChannel<IAlarmCallback>();
            alarmCallbacks.Add(a);
        }

        public void SendMessage(string message, byte[] signature)
        {
            realTimeDriver.SendMessage(message, signature);
        }

        public void SubscribeRealTimeUnit(string message, byte[] signature)
        {
            realTimeDriver.SubscribeRealTimeUnit(message, signature);
        }

        public double GetRealTimeUnitValue(string IOAdress)
        {
            return realTimeDriver.GetRealTimeUnitValue(IOAdress);
        }

        public double GetLatestTagOutputsValue(string tagName)
        {
            return tagValueService.GetLatestTagOutputsValue(tagName);
        }

        public bool checkOutputTagExistance(string input)
        {
            return tagService.checkOutputTagExistance(input);
        }

        public void AddRTUAdress(RTUAdress address)
        {
            rtuAdressService.AddRTUAdress(address);
        }

        public bool DeleteRTUAdress(string address)
        {
            return rtuAdressService.DeleteRTUAdress(address);
        }

        public RTUAdress GetRTUAdress(string address)
        {
            return rtuAdressService.GetRTUAdress(address);
        }

        public List<RTUAdress> GetRTUAdresses()
        {
            return rtuAdressService.GetRTUAdresses();
        }

        public RTUAdress UpdateRTUAdress(RTUAdress address)
        {
            return rtuAdressService.UpdateRTUAdress(address);
        }

        public string getTagValuesInTimePeriod(DateTime startDate, DateTime endDate)
        {
            return tagValueService.getTagValuesInTimePeriod(startDate, endDate);
        }

        public string getLatestAITagValues()
        {
            return tagValueService.getLatestAITagValues();

        }

        public string getLatestDITagValues()
        {
            return tagValueService.getLatestDITagValues();
        }

        public string getTagValuesByIdentifier(string id)
        {
            return tagValueService.getTagValuesByIdentifier(id);
        }
    }
}
