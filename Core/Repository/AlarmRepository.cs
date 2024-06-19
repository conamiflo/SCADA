using Core.Context;
using Core.Model.Alarm;
using Core.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.Repository
{
    public class AlarmRepository : IAlarmRepository
    {
        private readonly SCADAContext _context;
        private readonly string logFilePath = "alarmsLog.txt";

        public AlarmRepository(SCADAContext context)
        {
            _context = context;
        }

        public IEnumerable<Alarm> GetAllAlarms()
        {
            return _context.Alarms.ToList();
        }

        public void Add(Alarm alarm)
        {
            _context.Alarms.Add(alarm);
            _context.SaveChanges();
        }

        public Alarm GetById(int id)
        {
            return _context.Alarms.Find(id);
        }

        public void Remove(Alarm alarm)
        {
            _context.Alarms.Remove(alarm);
            _context.SaveChanges();
        }

        public void LogAlarm(Alarm alarm)
        {
            var logMessage = $"Alarm Triggered: Id={alarm.Id}, Type={alarm.Type}, Priority={alarm.Priority}, Threshold={alarm.Threshold}, Timestamp={DateTime.Now}";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }
}
