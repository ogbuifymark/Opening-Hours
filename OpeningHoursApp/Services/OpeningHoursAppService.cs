using OpeningHoursApp.Models;
using OpeningHoursApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpeningHoursApp.Services
{
    public class OpeningHoursAppService: IOpeningHoursAppService
    {

        public string ProcessSchedule(Dictionary<DayOfWeek, Schedule[]> model)
        {
            StringBuilder builder = new StringBuilder();
            bool isSpillOver = false;
            bool didDayEndWell = true;

            foreach (var day in model)
            {
                Schedule[] schedulesForDay = day.Value;
                schedulesForDay = schedulesForDay.OrderBy(x => x.Value).ToArray();
                int timeSlotsInDay = schedulesForDay.Count();


                if (timeSlotsInDay == 0)
                {
                    builder.Append($"{day.Key.ToString()}: Closed");
                    builder.Append("\n  ");
                    continue;
                }

                for (int i = 0; i < timeSlotsInDay; i++)
                {

                    Schedule schedule = schedulesForDay[i];
                    string timeRepresentation = ParseSecondsInDay(schedule.Value);

                    switch (schedule.Type)
                    {
                        case ScheduleType.open:
                            {
                                OpenHours(builder, ref isSpillOver, ref didDayEndWell, day, timeSlotsInDay, i, timeRepresentation);
                            }
                            break;
                        case ScheduleType.close:
                            {
                                ClosedHours(builder, ref isSpillOver, ref didDayEndWell, timeSlotsInDay, i, timeRepresentation);
                            }
                            break;
                        default:
                            throw new ArgumentException("Unsupported Schedule Type Value");
                    }

                   
                }



            }

            return builder.ToString();
        }

        private static void ClosedHours(StringBuilder builder, ref bool isSpillOver, ref bool didDayEndWell, int timeSlotsInDay, int i, string timeRepresentation)
        {
            if (isSpillOver)
            {
                builder.Append($"- {timeRepresentation}");
                builder.Append("\n  ");
                isSpillOver = false;
            }
            else
            {
                builder.Append($"- {timeRepresentation}, ");

                if (i == timeSlotsInDay - 1)
                {
                    didDayEndWell = false;
                    builder.Append("\n  ");
                }

            }
        }

        private static void OpenHours(StringBuilder builder, ref bool isSpillOver, ref bool didDayEndWell, KeyValuePair<DayOfWeek, Schedule[]> day, int timeSlotsInDay, int i, string timeRepresentation)
        {
            if (i == 0 || didDayEndWell == false)
            {
                builder.Append($"{day.Key.ToString()}: ");
                didDayEndWell = true;
            }

            builder.Append($"{timeRepresentation}");


            if (i == timeSlotsInDay - 1)
            {
                isSpillOver = true;
                didDayEndWell = false;
            }
        }

        public string ParseSecondsInDay(int seconds)
        {

            TimeSpan timeSpan =  TimeSpan.FromSeconds(seconds);

            if(timeSpan.Hours>=12)
            {
                double hour = timeSpan.Hours - 12==0?12: timeSpan.Hours - 12;
                return $"{hour} PM";
            }
            else
            {
                return $"{timeSpan.Hours} AM";
            }
        }

    }
}
