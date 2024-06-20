using Core.Model.Tag;
using Core.Repository.IRepository;
using Core.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Service
{
    public class TagValueService : ITagValueService
    {
        private readonly ITagValueRepository _tagValueRepository;

        public TagValueService(ITagValueRepository tagValueRepository)
        {
            _tagValueRepository = tagValueRepository;
        }

        public InputsValue GetInputsValue(int id)
        {
            return _tagValueRepository.GetInputsValue(id);
        }

        public OutputsValue GetOutputsValue(int id)
        {
            return _tagValueRepository.GetOutputsValue(id);
        }

        public List<InputsValue> GetAllInputsValues()
        {
            return _tagValueRepository.GetAllInputsValues();
        }

        public List<OutputsValue> GetAllOutputsValues()
        {
            return _tagValueRepository.GetAllOutputsValues();
        }

        public void AddInputsValue(InputsValue inputsValue)
        {
            _tagValueRepository.AddInputsValue(inputsValue);
        }

        public void AddOutputsValue(OutputsValue outputsValue)
        {
            _tagValueRepository.AddOutputsValue(outputsValue);
        }

        public bool RemoveInputsValue(int id)
        {
            return _tagValueRepository.RemoveInputsValue(id);
        }

        public bool RemoveOutputValue(int id)
        {
            return _tagValueRepository.RemoveOutputValue(id);
        }

        public InputsValue UpdateInputsValue(InputsValue inputsValue)
        {
            return _tagValueRepository.UpdateInputsValue(inputsValue);
        }

        public OutputsValue UpdateOutputsValue(OutputsValue outputsValue)
        {
            return _tagValueRepository.UpdateOutputsValue(outputsValue);
        }

        public double GetLatestTagOutputsValue(string tagName)
        {
            List<OutputsValue> outputsValues = new List<OutputsValue>();

            outputsValues = _tagValueRepository.GetAllOutputsValues();
            outputsValues= outputsValues.Where(x => x.TagName == tagName).ToList();
            outputsValues = outputsValues.OrderByDescending(x => x.TimeStamp).ToList();
            return outputsValues[0].Value;
        }
    }
}