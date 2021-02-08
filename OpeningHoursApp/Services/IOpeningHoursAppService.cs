using OpeningHoursApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpeningHoursApp.Services
{
    public interface IOpeningHoursAppService
    {
        public string ProcessSchedule(Dictionary<DayOfWeek, Schedule[]> model);
    }
}
