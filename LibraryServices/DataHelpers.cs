using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryServices
{
    public class DataHelpers
    {
        public static List<string> HumanizeBizHours (IEnumerable<BranchHour> BranchHours)
        {
            var hours = new List<string>();

            foreach (var item in BranchHours)
            {
                var HumanizedDay = HumanizeDay(item.DayOfWeek);
                var HumanizedOpeningTime = HumanizeTime(item.OpenTime);
                var HumanizedClosingTIme = HumanizeTime(item.CloseTime);

                var Humanizedthing = $"{HumanizedDay} {HumanizedOpeningTime} - {HumanizedClosingTIme}";
                hours.Add(Humanizedthing);
            }

            return hours;
        }

        public static object HumanizeTime(int Time)
        {
            return TimeSpan.FromHours(Time).ToString("hh':'mm");
        }

        public static object HumanizeDay(int dayOfWeek)
        {
            return Enum.GetName(typeof(DayOfWeek),dayOfWeek);
        }
    }
}
