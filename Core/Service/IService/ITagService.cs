using Core.Model.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.IService
{
    [ServiceContract]
    public interface ITagService
    {
        [OperationContract]
        void AddAnalogInput(AnalogInput analogInput);
        [OperationContract]
        bool DeleteAnalogInput(string id);
        [OperationContract]
        AnalogInput GetAnalogInput(string id);
        [OperationContract]
        AnalogInput UpdateAnalogInput(AnalogInput analogInput);
        [OperationContract]
        List<AnalogInput> GetAllAnalogInputs();
    }
}
