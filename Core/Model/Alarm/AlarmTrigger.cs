using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.Model.Alarm
{
    public class AlarmTrigger
    {
        [Key]
        public int Id { get; set; }
        public int AlarmId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}