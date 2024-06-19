using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model.Tag;

namespace Core.Repository.IRepository
{
    public interface ITagRepository
    {
        List<AnalogInput> GetAllAnalogInputs();
        AnalogInput GetAnalogInput(string id);
        void AddAnalogInput(AnalogInput analogInput);
        bool DeleteTag(string id);
        AnalogInput UpdateAnalogInput(AnalogInput analogInput);

        List<AnalogOutput> GetAllAnalogOutputs();
        AnalogOutput GetAnalogOutput(string id);
        void AddAnalogOutput(AnalogOutput analogOutput);
        AnalogOutput UpdateAnalogOutput(AnalogOutput analogOutput);

        List<DigitalInput> GetAllDigitalInputs();
        DigitalInput GetDigitalInput(string id);
        void AddDigitalInput(DigitalInput digitalInput);
        DigitalInput UpdateDigitalInput(DigitalInput digitalInput);

        List<DigitalOutput> GetAllDigitalOutputs();
        DigitalOutput GetDigitalOutput(string id);
        void AddDigitalOutput(DigitalOutput digitalOutput);
        DigitalOutput UpdateDigitalOutput(DigitalOutput digitalOutput);
    }
}
