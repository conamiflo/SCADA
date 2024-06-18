using Core.Model.Alarm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.IService
{
    internal interface IAlarmService
    {
        void Add(Alarm alarm);
        void Remove(int id);
        Alarm GetById(int id);
    }
}
