using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Core.Model;
using Core.Model.Tag;
namespace Core.Service
{
    public class TagProcessing
    {
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

        public static void StartProcessing() {

            //get all input tags
            List<AnalogInput> analogTags= new List<AnalogInput>();
            List<DigitalInput> digitalTags = new List<DigitalInput>();

            foreach (AnalogInput t in analogTags)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(getValuesAnalog));
                thread.Start(t);
            }

            foreach (DigitalInput t in digitalTags)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(getValuesDigital));
                thread.Start(t);
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

    }
}