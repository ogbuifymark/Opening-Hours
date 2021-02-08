using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpeningHoursApp.Models
{
    public class ScheduleRequestModel
    {
        public Dictionary<DayOfWeek, Schedule[]> SchedulePlan { get; set; }
    }
}
