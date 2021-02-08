using OpeningHoursApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpeningHoursApp.Models
{
    public class Schedule
    {
        public ScheduleType Type { get; set; }
        public int Value { get; set; }
    }
}
