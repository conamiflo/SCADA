using Core.Model.Tag;
using System.Collections.Generic;

namespace Core.Service.IService
{
    public interface ITagValueService
    {
        InputsValue GetInputsValue(int id);
        OutputsValue GetOutputsValue(int id);

        List<InputsValue> GetAllInputsValues();
        List<OutputsValue> GetAllOutputsValues();

        void AddInputsValue(InputsValue inputsValue);

        void AddOutputsValue(OutputsValue outputsValue);

        bool RemoveInputsValue(int id);
        bool RemoveOutputValue(int id);

        InputsValue UpdateInputsValue(InputsValue inputsValue);

        OutputsValue UpdateOutputsValue(OutputsValue outputsValue);
    }
}
