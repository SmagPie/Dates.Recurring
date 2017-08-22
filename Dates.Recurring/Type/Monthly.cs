using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Humanizer;

namespace Dates.Recurring.Type
{
    public class Monthly : RecurrenceType
    {
        public int DayOfMonth { get; set; }

        public Monthly(int skipMonths, int dayOfMonth, DateTime starting, DateTime? endingAfterDate, int? endingAfterNumOfOccurrences) : base(skipMonths, starting, endingAfterDate, endingAfterNumOfOccurrences)
        {
            DayOfMonth = dayOfMonth;
        }

        public override IEnumerable<DateTime> GetSchedule(DateTime? forecastLimit = null)
        {
            var occurrenceCount = 0;
            var next = Starting;
            var after = Starting - 1.Days();

            while (true)
            {
                if (DayOfMonthMatched(next))
                {
                    occurrenceCount++;

                    if ((EndingAfterDate.HasValue && next > EndingAfterDate.Value) ||
                        (EndingAfterNumOfOccurrences.HasValue && occurrenceCount > EndingAfterNumOfOccurrences) ||
                        next > forecastLimit ||
                        (DateTime.MaxValue.AddMonths(-X) - next).Days <= 0)
                        yield break;
                    if (next > after)
                        yield return next;
                }

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
        }

        [Obsolete("Try make this obsolete")]
        private bool Next(DateTime after, out DateTime next)
        {
            var occurrenceCount = 0;
            next = Starting;

            if (after < Starting)
                after = Starting - 1.Days();

            while (true)
            {
                if (DayOfMonthMatched(next))
                {
                    occurrenceCount++;

                    if ((EndingAfterDate.HasValue && next > EndingAfterDate.Value) ||
                        (EndingAfterNumOfOccurrences.HasValue && occurrenceCount > EndingAfterNumOfOccurrences))
                        return false;
                    if (next > after) return true;
                }

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

            return true;
        }

        [Obsolete("Try make this obsolete")]
        public override DateTime? Prev(DateTime before)
        {
            Next(before, out var next);

            if (before > next)
                before = next;

            while (next >= before || !DayOfMonthMatched(next))
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

            if (next < Starting)
                return null;

            return next;
        }

        private bool DayOfMonthMatched(DateTime date)
        {
            int dayOfMonth = Math.Min(DayOfMonth, DateTime.DaysInMonth(date.Year, date.Month));
            return (date.Day == dayOfMonth);
        }

    }
}
