using Core.Model;
using Core.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Service
{
    public class AlarmService
    {
        private readonly IAlarmRepository _alarmRepository;

        public AlarmService(IAlarmRepository alarmRepository)
        {
            _alarmRepository = alarmRepository;
        }

        public void AddAlarm(AlarmTrigger alarm)
        {
            _alarmRepository.Add(alarm);
        }

        public void RemoveAlarm(AlarmTrigger alarm)
        {
            _alarmRepository.Remove(alarm);
        }

        public AlarmTrigger GetAlarmById(int id)
        {
            return _alarmRepository.GetById(id);
        }

        public IEnumerable<AlarmTrigger> GetAllAlarms()
        {
            return _alarmRepository.GetAllAlarms();
        }

        public void LogAlarm(AlarmTrigger alarm)
        {
            _alarmRepository.LogAlarm(alarm);
        }
    }
}