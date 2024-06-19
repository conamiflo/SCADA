using Core.Model;
using Core.Repository.IRepository;
using Core.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Core.Service
{
    public class AlarmService : IAlarmService
    {
        public enum SortOption
        {
            Priority,
            Timestamp
        }

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
        public IEnumerable<AlarmTrigger> GetAlarmsInPeriod(DateTime startTime, DateTime endTime, SortOption sortOption)
        {
            var alarms = _alarmRepository.GetAllAlarms()
                                         .Where(a => a.Timestamp >= startTime && a.Timestamp <= endTime);

            return SortAlarms(alarms, sortOption);
        }

        private IEnumerable<AlarmTrigger> SortAlarms(IEnumerable<AlarmTrigger> alarms, SortOption sortOption)
        {
            switch (sortOption)
            {
                case SortOption.Priority:
                    return alarms.OrderByDescending(a => a.Priority).ThenBy(a => a.Timestamp);
                case SortOption.Timestamp:
                    return alarms.OrderBy(a => a.Timestamp);
                default:
                    throw new ArgumentException("Invalid sort option");
            }
        }

        public IEnumerable<AlarmTrigger> GetAlarmsByPriority(int priority)
        {
            return _alarmRepository.GetAllAlarms()
                                   .Where(a => a.Priority == priority)
                                   .OrderBy(a => a.Timestamp);
        }
    }
}