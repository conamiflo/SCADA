using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.ApplicationServices;
using Core.Model;
using Core.Model.Tag;
using Core.Service.IService;
namespace Core.Service
{
    public class TagProcessing
    {
        public static Dictionary<string,Thread> activeTags = new Dictionary<string, Thread>();
        public static CoreService coreService;

        public TagProcessing(ITagService tagService,CoreService coreServiceInstance)
        {
            StartProcessing(tagService);
            coreService = coreServiceInstance;
        }

        public void getValuesAnalog(object t)
        {
            AnalogInput tag = (AnalogInput)t;
            while (true)
            {
                double value = 0;

                if (!tag.IsOn)
                {
                    activeTags.Remove(tag.TagName);
                    break;
                }

                if (tag.Driver.Equals("Sim"))
                {
                    value=SimulationDriver.SimulationDriver.ReturnValue(tag.IOAddress);

                    if (value > tag.HighLimit)
                    {
                        value = tag.HighLimit;
                    }
                    else if(value < tag.LowLimit)
                    {
                        value = tag.LowLimit;
                    }
                    
                }
                else if(tag.Driver.Equals("RTD"))
                {
                    //TODO rtd logika
                }
                else
                {
                    break;
                }

                InputsValue inputsValue = new InputsValue(tag.IOAddress, tag.TagName, value, Model.Tag.ValueType.DIGITAL);
                coreService.AddInputsValue(inputsValue);

                processAlarms(value, tag.Alarms, tag.TagName);
                coreService.addTagValue(tag.TagName, value);

                Thread.Sleep((int)tag.ScanTime);
            }
        }

        public static void getValuesDigital(object t)
        {
            DigitalInput tag = (DigitalInput)t;
            while (true)
            {
                if (!tag.IsOn)
                {
                    activeTags.Remove(tag.TagName);
                    break;
                }

                double value = 0;

                if (tag.Driver.Equals("Sim"))
                {
                    value = SimulationDriver.SimulationDriver.ReturnValue(tag.IOAddress);

                }
                else if (tag.Driver.Equals("RTD"))
                {
                    //TODO rtd logika
                }
                else
                {
                    break;
                }

                InputsValue inputsValue = new InputsValue(tag.IOAddress, tag.TagName, value, Model.Tag.ValueType.DIGITAL);
                coreService.AddInputsValue(inputsValue);

                coreService.addTagValue(tag.TagName, value);

                Thread.Sleep((int)tag.ScanTime);
            }
        }

        public void StartProcessing(ITagService tagService) {
            List<AnalogInput> analogTags = tagService.GetAllAnalogInputs();
            List<DigitalInput> digitalTags = tagService.GetAllDigitalInputs();

            foreach (AnalogInput t in analogTags)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(getValuesAnalog));
                thread.Start(t);

                if (!activeTags.ContainsKey(t.TagName))
                {
                    activeTags.Add(t.TagName, thread);
                }
            }

            foreach (DigitalInput t in digitalTags)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(getValuesDigital));
                thread.Start(t);

                if (!activeTags.ContainsKey(t.TagName))
                {
                    activeTags.Add(t.TagName, thread);
                }
            }
        }

        void processAlarms(double value,List<Alarm> alarms, string tagName)
        {
            if (alarms == null) return;

            List<AlarmTrigger> triggers = new List<AlarmTrigger>();

            foreach (Alarm a in alarms)
            {
                if (value > a.Threshold)
                {
                    triggers.Add(new AlarmTrigger(a,tagName,DateTime.Now,value));
                }
            }

            // TODO prikazati alarme i sacuvati trigere
        }

        public void deleteTag(string tagName)
        {
            if (activeTags.ContainsKey(tagName))
            {
                Thread thread = activeTags[tagName];
                thread.Abort();
                activeTags.Remove(tagName);
                coreService.removeTag(tagName);
            }
        }

        public void addAnalogTag(AnalogInput t)
        {
            if (! activeTags.ContainsKey(t.TagName))
            {
                Thread thread = new Thread(new ParameterizedThreadStart(getValuesAnalog));
                thread.Start(t);

                if (!activeTags.ContainsKey(t.TagName))
                {
                    activeTags.Add(t.TagName, thread);
                }
            }
        }

        public void addDigitalTag(DigitalInput t)
        {
            if (!activeTags.ContainsKey(t.TagName))
            {
                Thread thread = new Thread(new ParameterizedThreadStart(getValuesDigital));
                thread.Start(t);

                if (!activeTags.ContainsKey(t.TagName))
                {
                    activeTags.Add(t.TagName, thread);
                }
            }
        }

    }
}