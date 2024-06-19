using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Core.Model;
using Core.Model.Tag;
using Core.Service.IService;
namespace Core.Service
{
    public class TagProcessing
    {
        public static Dictionary<string,Thread> activeTags = new Dictionary<string, Thread>();

        public TagProcessing(ITagService tagService)
        {
            StartProcessing(tagService);
        }

        public static void getValuesAnalog(object t)
        {
            AnalogInput tag = (AnalogInput)t;
            while (true)
            {
                if (!tag.IsOn)break;

                if (tag.Driver.Equals("Sim"))
                {
                    double value=SimulationDriver.SimulationDriver.ReturnValue(tag.IOAddress);

                    if (value > tag.HighLimit)
                    {
                        value = tag.HighLimit;
                    }
                    else if(value < tag.LowLimit)
                    {
                        value = tag.LowLimit;
                    }
                    processAlarms(value, tag.Alarms, tag.TagName);
                }
                Thread.Sleep((int)tag.ScanTime);
            }
        }

        public static void getValuesDigital(object t)
        {
            DigitalInput tag = (DigitalInput)t;
            while (true)
            {
                if (!tag.IsOn) break;

                if (tag.Driver.Equals("Sim"))
                {
                    double value = SimulationDriver.SimulationDriver.ReturnValue(tag.IOAddress);
                }
                Thread.Sleep((int)tag.ScanTime);
            }
        }

        public static void StartProcessing(ITagService tagService) {
            List<AnalogInput> analogTags = tagService.GetAllAnalogInputs();
            List<DigitalInput> digitalTags = tagService.GetAllDigitalInputs();

            foreach (AnalogInput t in analogTags)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(getValuesAnalog));
                thread.Start(t);
                activeTags.Add(t.TagName, thread);
            }

            foreach (DigitalInput t in digitalTags)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(getValuesDigital));
                thread.Start(t);
                activeTags.Add(t.TagName, thread);
            }
        }

        static void processAlarms(double value,List<Alarm> alarms, string tagName)
        {
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

        public static void deleteTag(string tagName)
        {
            if (activeTags.ContainsKey(tagName))
            {
                Thread thread = activeTags[tagName];
                thread.Abort();
                activeTags.Remove(tagName);
            }
        }

        public static void addAnalogTag(AnalogInput tag)
        {
            if (! activeTags.ContainsKey(tag.TagName))
            {
                Thread thread = new Thread(new ParameterizedThreadStart(getValuesAnalog));
                thread.Start(tag);
                activeTags.Add(tag.TagName, thread);
            }
        }

        public static void addDigitalTag(DigitalInput tag)
        {
            if (!activeTags.ContainsKey(tag.TagName))
            {
                Thread thread = new Thread(new ParameterizedThreadStart(getValuesDigital));
                thread.Start(tag);
                activeTags.Add(tag.TagName, thread);
            }
        }

    }
}