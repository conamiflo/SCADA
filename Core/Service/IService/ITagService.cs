using Core.Model.Tag;
using System.Collections.Generic;
using System.ServiceModel;

namespace Core.Service.IService
{
    [ServiceContract]
    public interface ITagService
    {
        [OperationContract]
         bool checkOutputTagExistance(string input);
        [OperationContract]
        void AddAnalogInput(AnalogInput analogInput);

        [OperationContract]
        bool DeleteTag(string id);

        [OperationContract]
        void ToggleTagScan(string id, bool isOn,bool isAnalog);

        [OperationContract]
        AnalogInput GetAnalogInput(string id);

        [OperationContract]
        AnalogInput UpdateAnalogInput(AnalogInput analogInput);

        [OperationContract]
        List<AnalogInput> GetAllAnalogInputs();


        [OperationContract]
        void AddAnalogOutput(AnalogOutput analogOutput);

        [OperationContract]
        AnalogOutput GetAnalogOutput(string id);

        [OperationContract]
        AnalogOutput UpdateAnalogOutput(AnalogOutput analogOutput);

        [OperationContract]
        List<AnalogOutput> GetAllAnalogOutputs();


        [OperationContract]
        void AddDigitalInput(DigitalInput digitalInput);

        [OperationContract]
        DigitalInput GetDigitalInput(string id);

        [OperationContract]
        DigitalInput UpdateDigitalInput(DigitalInput digitalInput);

        [OperationContract]
        List<DigitalInput> GetAllDigitalInputs();


        [OperationContract]
        void AddDigitalOutput(DigitalOutput digitalOutput);

        [OperationContract]
        DigitalOutput GetDigitalOutput(string id);

        [OperationContract]
        DigitalOutput UpdateDigitalOutput(DigitalOutput digitalOutput);

        [OperationContract]
        List<DigitalOutput> GetAllDigitalOutputs();
    }
}
