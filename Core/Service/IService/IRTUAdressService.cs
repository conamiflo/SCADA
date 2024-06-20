using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.IService
{
    [ServiceContract]
    public interface IRTUAdressService
    {
        [OperationContract]
        void AddRTUAdress(RTUAdress address);

        [OperationContract]
        bool DeleteRTUAdress(string address);

        [OperationContract]
        RTUAdress GetRTUAdress(string address);

        [OperationContract]
        List<RTUAdress> GetRTUAdresses();

        [OperationContract]
        RTUAdress UpdateRTUAdress(RTUAdress address);
    }
}
