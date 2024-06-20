using Core.Model.Tag;
using Core.Repository.IRepository;
using Core.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public string getTagValuesInTimePeriod(DateTime startDate, DateTime endDate)
        {
            var inputsValues = _tagValueRepository.GetAllInputsValues()
                .Where(iv => iv.TimeStamp >= startDate && iv.TimeStamp <= endDate)
                .ToList();

            var outputsValues = _tagValueRepository.GetAllOutputsValues()
                .Where(ov => ov.TimeStamp >= startDate && ov.TimeStamp <= endDate)
                .ToList();

            StringBuilder report = new StringBuilder();
            report.AppendLine("Inputs Values:");
            foreach (var iv in inputsValues)
            {
                report.AppendLine($"Id: {iv.Id}, IOAddress: {iv.IOAddress}, TagName: {iv.TagName}, TimeStamp: {iv.TimeStamp}, Value: {iv.Value}, ValueType: {iv.ValueType}");
            }

            report.AppendLine("Outputs Values:");
            foreach (var ov in outputsValues)
            {
                report.AppendLine($"Id: {ov.Id}, IOAddress: {ov.IOAddress}, TagName: {ov.TagName}, TimeStamp: {ov.TimeStamp}, Value: {ov.Value}, ValueType: {ov.ValueType}");
            }

            return report.ToString();
        }

        public string getLatestAITagValues()
        {
            var latestAnalogInputs = _tagValueRepository.GetAllInputsValues()
                .Where(iv => iv.ValueType == Model.Tag.ValueType.ANALOG)
                .GroupBy(iv => iv.TagName)
                .Select(g => g.OrderByDescending(iv => iv.TimeStamp).First())
                .ToList();

            StringBuilder report = new StringBuilder();
            report.AppendLine("Latest Analog Input Values:");
            foreach (var iv in latestAnalogInputs)
            {
                report.AppendLine($"Id: {iv.Id}, IOAddress: {iv.IOAddress}, TagName: {iv.TagName}, TimeStamp: {iv.TimeStamp}, Value: {iv.Value}, ValueType: {iv.ValueType}");
            }

            return report.ToString();
        }

        public string getLatestDITagValues()
        {
            var latestDigitalInputs = _tagValueRepository.GetAllInputsValues()
                .Where(iv => iv.ValueType == Model.Tag.ValueType.DIGITAL)
                .GroupBy(iv => iv.TagName)
                .Select(g => g.OrderByDescending(iv => iv.TimeStamp).First())
                .ToList();

            StringBuilder report = new StringBuilder();
            report.AppendLine("Latest Digital Input Values:");
            foreach (var iv in latestDigitalInputs)
            {
                report.AppendLine($"Id: {iv.Id}, IOAddress: {iv.IOAddress}, TagName: {iv.TagName}, TimeStamp: {iv.TimeStamp}, Value: {iv.Value}, ValueType: {iv.ValueType}");
            }

            return report.ToString();
        }

        public string getTagValuesByIdentifier(string id)
        {
            var inputsValues = _tagValueRepository.GetAllInputsValues()
                 .Where(iv => iv.TagName == id)
                 .ToList();

            var outputsValues = _tagValueRepository.GetAllOutputsValues()
                .Where(ov => ov.TagName == id)
                .ToList();

            StringBuilder report = new StringBuilder();
            report.AppendLine($"Tag Values for Identifier: {id}");
            report.AppendLine("Inputs Values:");
            foreach (var iv in inputsValues)
            {
                report.AppendLine($"Id: {iv.Id}, IOAddress: {iv.IOAddress}, TagName: {iv.TagName}, TimeStamp: {iv.TimeStamp}, Value: {iv.Value}, ValueType: {iv.ValueType}");
            }

            report.AppendLine("Outputs Values:");
            foreach (var ov in outputsValues)
            {
                report.AppendLine($"Id: {ov.Id}, IOAddress: {ov.IOAddress}, TagName: {ov.TagName}, TimeStamp: {ov.TimeStamp}, Value: {ov.Value}, ValueType: {ov.ValueType}");
            }

            return report.ToString();
        }
    }
}