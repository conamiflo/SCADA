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

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
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
        }

        public bool DeleteAnalogInput(string id)
        {
            return _tagRepository.DeleteAnalogInput(id);
        }

        public AnalogInput GetAnalogInput(string id)
        {
            return _tagRepository.GetAnalogInput(id);
        }

        public AnalogInput UpdateAnalogInput(AnalogInput analogInput)
        {
            return _tagRepository.UpdateAnalogInput(analogInput);
        }
    }
}