using Core.Model.Tag;
using Core.Repository;
using Core.Repository.IRepository;
using Core.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Security.Cryptography;
using System.Web;

namespace Core.Service
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private TagProcessing TagProcessing;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
            TagProcessing = new TagProcessing(this);
        }

        public void AddAnalogInput(AnalogInput analogInput)
        {
            if (string.IsNullOrEmpty(analogInput.TagName))
            {
                throw new ArgumentException("Tag name cannot be null or empty.");
            }
            if (string.IsNullOrEmpty(analogInput.Description))
            {
                throw new ArgumentException("Description cannot be null or empty.");
            }
            if (string.IsNullOrEmpty(analogInput.IOAddress))
            {
                throw new ArgumentException("IOAddress cannot be null or empty.");
            }
            if (double.IsNaN(analogInput.ScanTime))
            {
                throw new ArgumentException("Scan time must be a number.");
            }
            if (double.IsNaN(analogInput.LowLimit))
            {
                throw new ArgumentException("Low limit must be a number.");
            }
            if (double.IsNaN(analogInput.HighLimit))
            {
                throw new ArgumentException("High limit must be a number.");
            }
            if (string.IsNullOrEmpty(analogInput.Units))
            {
                throw new ArgumentException("Units cannot be null or empty.");
            }
            _tagRepository.AddAnalogInput(analogInput);
            TagProcessing.addAnalogTag(analogInput);
        }

        public bool DeleteTag(string id)
        {
            TagProcessing.deleteTag(id);
            return _tagRepository.DeleteTag(id);
        }

        public AnalogInput GetAnalogInput(string id)
        {
            return _tagRepository.GetAnalogInput(id);
        }

        public AnalogInput UpdateAnalogInput(AnalogInput analogInput)
        {
            return _tagRepository.UpdateAnalogInput(analogInput);
        }

        public List<AnalogInput> GetAllAnalogInputs()
        {
            return _tagRepository.GetAllAnalogInputs();
        }

        public void AddAnalogOutput(AnalogOutput analogOutput)
        {
            ValidateTagProperties(analogOutput);
            _tagRepository.AddAnalogOutput(analogOutput);
        }

        public AnalogOutput GetAnalogOutput(string id)
        {
            return _tagRepository.GetAnalogOutput(id);
        }

        public AnalogOutput UpdateAnalogOutput(AnalogOutput analogOutput)
        {
            ValidateTagProperties(analogOutput);
            return _tagRepository.UpdateAnalogOutput(analogOutput);
        }

        public List<AnalogOutput> GetAllAnalogOutputs()
        {
            return _tagRepository.GetAllAnalogOutputs();
        }

        public void AddDigitalInput(DigitalInput digitalInput)
        {
            ValidateTagProperties(digitalInput);
            _tagRepository.AddDigitalInput(digitalInput);
            TagProcessing.addDigitalTag(digitalInput);
        }

        public DigitalInput GetDigitalInput(string id)
        {
            return _tagRepository.GetDigitalInput(id);
        }

        public DigitalInput UpdateDigitalInput(DigitalInput digitalInput)
        {
            ValidateTagProperties(digitalInput);
            return _tagRepository.UpdateDigitalInput(digitalInput);
        }

        public List<DigitalInput> GetAllDigitalInputs()
        {
            return _tagRepository.GetAllDigitalInputs();
        }

        public void AddDigitalOutput(DigitalOutput digitalOutput)
        {
            ValidateTagProperties(digitalOutput);
            _tagRepository.AddDigitalOutput(digitalOutput);
        }
        public DigitalOutput GetDigitalOutput(string id)
        {
            return _tagRepository.GetDigitalOutput(id);
        }

        public DigitalOutput UpdateDigitalOutput(DigitalOutput digitalOutput)
        {
            ValidateTagProperties(digitalOutput);
            return _tagRepository.UpdateDigitalOutput(digitalOutput);
        }

        public List<DigitalOutput> GetAllDigitalOutputs()
        {
            return _tagRepository.GetAllDigitalOutputs();
        }

        private void ValidateTagProperties(Tag tag)
        {
            if (string.IsNullOrEmpty(tag.TagName))
            {
                throw new ArgumentException("Tag name cannot be null or empty.");
            }
            if (string.IsNullOrEmpty(tag.Description))
            {
                throw new ArgumentException("Description cannot be null or empty.");
            }
            if (string.IsNullOrEmpty(tag.IOAddress))
            {
                throw new ArgumentException("IOAddress cannot be null or empty.");
            }
        }
    }
}