using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;

namespace Dates.Recurring.Type
{
    public class Monthly : RecurrenceType
    {
        public int DayOfMonth { get; set; }

        public Monthly(int skipMonths, int dayOfMonth, DateTime starting, DateTime? ending) : base(skipMonths, starting, ending)
        {
            DayOfMonth = dayOfMonth;
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

            while (next.Date <= after.Date || !DayOfMonthMatched(next))
            {
                int dayOfMonth = Math.Min(DayOfMonth, DateTime.DaysInMonth(next.Year, next.Month));

                if (next.Day < dayOfMonth)
                {
                    next = next + 1.Days();
                }
                else
                {
                    // Rewind to the first of the month.
                    next = next + ((-1 * next.Day) + 1).Days();

                    // Skip ahead by the required number of months.
                    next = next.AddMonths(X);
                }
            }

            if (!ignoreEndLimit && Ending.HasValue && next.Date >= Ending.Value.Date)
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

            while (next.Date >= before.Date || !DayOfMonthMatched(next))
            {
                int dayOfMonth = Math.Min(DayOfMonth, DateTime.DaysInMonth(next.Year, next.Month));

                if (next.Day > dayOfMonth)
                {
                    next = next - 1.Days();
                }
                else
                {
                    // Skip ahead by the required number of months.
                    next = next.AddMonths(-X);

                    // Fast forward to the end of the month.
                    next = next + (DateTime.DaysInMonth(next.Year, next.Month) - next.Day).Days();
                }
            }

            if (next.Date < Starting.Date)
            {
                return null;
            }

            return next;
        }

        private bool DayOfMonthMatched(DateTime date)
        {
            int dayOfMonth = Math.Min(DayOfMonth, DateTime.DaysInMonth(date.Year, date.Month));
            return (date.Day == dayOfMonth);
        }

    }
}
