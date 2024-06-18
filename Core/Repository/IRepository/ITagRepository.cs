﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model.Tag;

namespace Core.Repository.IRepository
{
    internal interface ITagRepository
    {
        AnalogInput GetAnalogInput(string id);
        void AddAnalogInput(AnalogInput analogInput);
        bool DeleteAnalogInput(string id);
        AnalogInput UpdateAnalogInput(AnalogInput analogInput);

    }
}