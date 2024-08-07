﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace Core.Service.IService
{
    [ServiceContract]
    public interface IRealTimeDriver
    {
        [OperationContract]
        void SendMessage(string message, byte[] signature);

        [OperationContract]
        void SubscribeRealTimeUnit(string message, byte[] signature);

        [OperationContract]
        double GetRealTimeUnitValue(string IOAdress);
    }
}
