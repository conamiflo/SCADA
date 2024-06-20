using Core.Model.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.IRepository
{
    public interface ITagValueRepository
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
