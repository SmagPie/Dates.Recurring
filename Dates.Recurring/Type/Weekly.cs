using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;

namespace Dates.Recurring.Type
{
    public class Weekly : RecurrenceType
    {
        public Day Days { get; set; }

        public Weekly(int weeks, DateTime starting, DateTime? ending, Day days) : base(weeks, starting, ending)
        {
            Days = days;
        }

        public override DateTime? Next(DateTime after)
        {
            return Next(after, false);
        }

        private DateTime? Next(DateTime after, bool ignoreEndLimit)
        {
            var next = Starting;

            if (after.Date < Starting.Date)
            {
                after = Starting - 1.Days();
            }

            while (next.Date <= after.Date || !DayOfWeekMatched(next.DayOfWeek))
            {
                if (next.DayOfWeek != DayOfWeek.Saturday)
                {
                    next = next + 1.Days();
                }
                else
                {
                    // Skip ahead by x weeks.
                    next = next + X.Weeks();

                    // Rewind to the first day of the week.
                    int delta = DayOfWeek.Sunday - next.DayOfWeek;
                    if (delta > 0)
                    {
                        delta -= 7;
                    }
                    next = next + delta.Days();
                }
            }

            if (!ignoreEndLimit && Ending.HasValue && next.Date > Ending.Value.Date)
            {
                return null;
            }

            return next;
        }

        public override DateTime? Prev(DateTime before)
        {
            // ReSharper disable once PossibleInvalidOperationException
            var next = Next(before, true).Value;

            if (before.Date > Ending.Value.Date)
            {
                before = Ending.Value + 1.Days();
            }

            while (next.Date >= before.Date || !DayOfWeekMatched(next.DayOfWeek))
            {
                if (next.DayOfWeek != DayOfWeek.Monday)
                {
                    // Step from the end of the week to the start of the week
                    next = next - 1.Days();
                }
                else
                {
                    // Skip ahead by x weeks.
                    next = next - X.Weeks();

                    // Rewind to the end of the week
                    next = next.AddDays(6);
                }
            }

            if (next.Date < Starting.Date)
            {
                return null;
            }

            return next;
        }

        private bool DayOfWeekMatched(DayOfWeek day)
        {
            if (day == DayOfWeek.Sunday && (Days & Day.SUNDAY) != 0)
                return true;

            if (day == DayOfWeek.Monday && (Days & Day.MONDAY) != 0)
                return true;

            if (day == DayOfWeek.Tuesday && (Days & Day.TUESDAY) != 0)
                return true;

            if (day == DayOfWeek.Wednesday && (Days & Day.WEDNESDAY) != 0)
                return true;

            if (day == DayOfWeek.Thursday && (Days & Day.THURSDAY) != 0)
                return true;

            if (day == DayOfWeek.Friday && (Days & Day.FRIDAY) != 0)
                return true;

            if (day == DayOfWeek.Saturday && (Days & Day.SATURDAY) != 0)
                return true;

            return false;
        }
    }
}
