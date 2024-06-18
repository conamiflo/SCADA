using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.IRepository
{
    internal interface ISCADARepository
    {
        AnalogInput GetAnalogInput(string id);
        AnalogInput AddAnalogInput(AnalogInput analogInput);
        bool DeleteAnalogInput(string id);
        AnalogInput UpdateAnalogInput(AnalogInput analogInput);

    }
}
