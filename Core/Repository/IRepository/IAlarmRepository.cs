using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.IRepository
{
    public interface IAlarmRepository
    {
        void Add(AlarmTrigger alarm);
        void Remove(AlarmTrigger alarm);
        AlarmTrigger GetById(int id);
        IEnumerable<AlarmTrigger> GetAllAlarms();
        void LogAlarm(AlarmTrigger alarm);
    }
}
