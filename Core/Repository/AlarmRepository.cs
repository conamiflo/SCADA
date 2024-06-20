using Core.Context;
using Core.Model;
using Core.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace Core.Repository
{
    public class AlarmRepository : IAlarmRepository
    {
        private readonly SCADAContext _context;
        private readonly string logFilePath = HttpContext.Current.Server.MapPath("~/AppData/alarmsLog.txt");

        public AlarmRepository()
        {
            _context = new SCADAContext();
            _context.AlarmTriggers.RemoveRange(_context.AlarmTriggers);
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
            var logMessage = $"Alarm Triggered: Id={alarm.Id},Tag={alarm.TagName},Tag value= {alarm.TagValue} ,Type={alarm.Type}, Priority={alarm.Priority}, Threshold={alarm.Threshold}, Timestamp={alarm.Timestamp}";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            Add(alarm);
        }
    }
}
