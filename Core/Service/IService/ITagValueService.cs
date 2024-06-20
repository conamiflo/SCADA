using Core.Model.Tag;
using System.Collections.Generic;
using System.ServiceModel;

namespace Core.Service.IService
{
    [ServiceContract]
    public interface ITagValueService
    {
        [OperationContract]
        InputsValue GetInputsValue(int id);
        [OperationContract]
        OutputsValue GetOutputsValue(int id);
        [OperationContract]
        List<InputsValue> GetAllInputsValues();
        [OperationContract]
        List<OutputsValue> GetAllOutputsValues();
        [OperationContract]
        void AddInputsValue(InputsValue inputsValue);
        [OperationContract]
        void AddOutputsValue(OutputsValue outputsValue);
        [OperationContract]
        bool RemoveInputsValue(int id);
        [OperationContract]
        bool RemoveOutputValue(int id);
        [OperationContract]
        InputsValue UpdateInputsValue(InputsValue inputsValue);
        [OperationContract]
        OutputsValue UpdateOutputsValue(OutputsValue outputsValue);
    }
}
