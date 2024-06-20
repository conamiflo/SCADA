using Core.Context;
using Core.Model.Tag;
using Core.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Repository
{
    public class TagValueRepository : ITagValueRepository
    {
        private readonly SCADAContext _scadaContext;

        public TagValueRepository()
        {
            _scadaContext = new SCADAContext();
        }

        public void AddInputsValue(InputsValue inputsValue)
        {
            _scadaContext.InputsValues.Add(inputsValue);
            _scadaContext.SaveChanges();
        }

        public void AddOutputsValue(OutputsValue outputsValue)
        {
            _scadaContext.OutputsValues.Add(outputsValue);
            _scadaContext.SaveChanges();
        }

        public List<InputsValue> GetAllInputsValues()
        {
            return _scadaContext.InputsValues.ToList();
        }

        public List<OutputsValue> GetAllOutputsValues()
        {
            return _scadaContext.OutputsValues.ToList();
        }

        public InputsValue GetInputsValue(int id)
        {
            return _scadaContext.InputsValues.FirstOrDefault(iv => iv.Id == id);
        }

        public OutputsValue GetOutputsValue(int id)
        {
            return _scadaContext.OutputsValues.FirstOrDefault(ov => ov.Id == id);
        }

        public bool RemoveInputsValue(int id)
        {
            var inputsValue = _scadaContext.InputsValues.FirstOrDefault(iv => iv.Id == id);
            if (inputsValue == null)
            {
                return false;
            }
            _scadaContext.InputsValues.Remove(inputsValue);
            _scadaContext.SaveChanges();
            return true;
        }

        public bool RemoveOutputValue(int id)
        {
            var outputsValue = _scadaContext.OutputsValues.FirstOrDefault(ov => ov.Id == id);
            if (outputsValue == null)
            {
                return false;
            }
            _scadaContext.OutputsValues.Remove(outputsValue);
            _scadaContext.SaveChanges();
            return true;
        }

        public InputsValue UpdateInputsValue(InputsValue inputsValue)
        {
            var existingInputsValue = _scadaContext.InputsValues.FirstOrDefault(iv => iv.Id == inputsValue.Id);
            if (existingInputsValue == null)
            {
                return null;
            }
            _scadaContext.Entry(existingInputsValue).CurrentValues.SetValues(inputsValue);
            _scadaContext.SaveChanges();
            return existingInputsValue;
        }

        public OutputsValue UpdateOutputsValue(OutputsValue outputsValue)
        {
            var existingOutputsValue = _scadaContext.OutputsValues.FirstOrDefault(ov => ov.Id == outputsValue.Id);
            if (existingOutputsValue == null)
            {
                return null;
            }
            _scadaContext.Entry(existingOutputsValue).CurrentValues.SetValues(outputsValue);
            _scadaContext.SaveChanges();
            return existingOutputsValue;
        }
    }
}