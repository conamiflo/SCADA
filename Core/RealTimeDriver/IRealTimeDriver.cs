using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RealTimeDriver
{
    [ServiceContract]
    public interface IRealTimeDriver
    {
        [OperationContract]
        void SendMessage(string message, byte[] signature);
    }
}
