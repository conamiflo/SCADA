using Core.Model.Tag;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace Core.Service.IService
{
    [ServiceContract]
    public interface ITagValueService
    {
        InputsValue GetInputsValue(int id);
        [OperationContract]
        OutputsValue GetOutputsValue(int id);
        [OperationContract]
        double GetLatestTagOutputsValue(string tagName);
        List<InputsValue> GetAllInputsValues();
        List<OutputsValue> GetAllOutputsValues();
        void AddInputsValue(InputsValue inputsValue);
        [OperationContract]
        void AddOutputsValue(OutputsValue outputsValue);
        bool RemoveInputsValue(int id);
        bool RemoveOutputValue(int id);
        InputsValue UpdateInputsValue(InputsValue inputsValue);
        OutputsValue UpdateOutputsValue(OutputsValue outputsValue);

        [OperationContract]
        string getTagValuesInTimePeriod(DateTime startDate, DateTime endDate);

        [OperationContract]
        string getLatestAITagValues();

        [OperationContract]
        string getLatestDITagValues();

        [OperationContract]
        string getTagValuesByIdentifier(string id);
        
    }
}
