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
    public interface IAlarmService
    {
        [OperationContract]
        void AddAlarm(AlarmTrigger alarm);
        [OperationContract]
        void RemoveAlarm(AlarmTrigger alarm);
        [OperationContract]
        AlarmTrigger GetAlarmById(int id);
        [OperationContract]
        IEnumerable<AlarmTrigger> GetAllAlarms();
        [OperationContract]
        void LogAlarm(AlarmTrigger alarm);
    }
}
