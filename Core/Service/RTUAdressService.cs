using Core.Model;
using Core.Repository.IRepository;
using Core.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Service
{
    public class RTUAdressService : IRTUAdressService
    {
        private readonly IRTUAdressRepository _rtuAdressRepository;

        public RTUAdressService(IRTUAdressRepository rtuAdressRepository)
        {
            _rtuAdressRepository = rtuAdressRepository;
        }

        public void AddRTUAdress(RTUAdress address)
        {
            _rtuAdressRepository.AddRTUAdress(address);
        }

        public bool DeleteRTUAdress(string address)
        {
            return _rtuAdressRepository.DeleteRTUAdress(address);
        }

        public RTUAdress GetRTUAdress(string address)
        {
            return _rtuAdressRepository.GetRTUAdress(address);
        }

        public List<RTUAdress> GetRTUAdresses()
        {
            return _rtuAdressRepository.GetRTUAdresses();
        }

        public RTUAdress UpdateRTUAdress(RTUAdress address)
        {
            return _rtuAdressRepository.UpdateRTUAdress(address);
        }
    }
}