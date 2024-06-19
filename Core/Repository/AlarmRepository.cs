﻿using Core.Context;
using Core.Model;
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

        public IEnumerable<AlarmTrigger> GetAllAlarms()
        {
            return _context.AlarmTriggers.ToList();
        }

        public void Add(AlarmTrigger alarm)
        {
            _context.AlarmTriggers.Add(alarm);
            _context.SaveChanges();
        }

        public AlarmTrigger GetById(int id)
        {
            return _context.AlarmTriggers.Find(id);
        }

        public void Remove(AlarmTrigger alarm)
        {
            _context.AlarmTriggers.Remove(alarm);
            _context.SaveChanges();
        }

        public void LogAlarm(AlarmTrigger alarm)
        {
            var logMessage = $"Alarm Triggered: Id={alarm.Id}, AnalogInputId={alarm.TagName}, Type={alarm.Type}, Priority={alarm.Priority}, Unit={alarm.Unit}, Timestamp={DateTime.Now}";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }
}
