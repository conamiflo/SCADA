using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static Core.Service.AlarmService;

namespace Core.Service.IService
{
    public interface ITrendingCallback
    {

        [OperationContract(IsOneWay = true)]
        void initTagTable(Dictionary<string, double> tags);

        [OperationContract(IsOneWay = true)]
        void addTagValue(string tagName, double value);

        [OperationContract(IsOneWay = true)]
        void removeTag(string tagName);
    }

    [ServiceContract(CallbackContract = typeof(ITrendingCallback))]
    public interface ITrendingService
    {
        [OperationContract]
        void SubscribeToTrending();
    }
}
