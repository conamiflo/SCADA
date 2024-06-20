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

    public interface IAlarmCallback
    {
        [OperationContract(IsOneWay = true)]
        void AlarmTriggered(string message);
    }

    [ServiceContract(CallbackContract = typeof(IAlarmCallback))]
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

        [OperationContract]
        IEnumerable<AlarmTrigger> GetAlarmsInPeriod(DateTime startTime, DateTime endTime, bool sortOption);

        [OperationContract]
        IEnumerable<AlarmTrigger> GetAlarmsByPriority(int priority);

        [OperationContract]
        void SubscribeToAlarmDisplay();
    }
}
