using Core.Model.Alarm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.IRepository
{
    internal interface IAlarmRepository
    {
        void Add(Alarm alarm);
        void Remove(Alarm alarm);
        Alarm GetById(int id);
        IEnumerable<Alarm> GetAllAlarms();
        void LogAlarm(Alarm alarm);
    }
}
