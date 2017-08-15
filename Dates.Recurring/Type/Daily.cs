using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;

namespace Dates.Recurring.Type
{
    public class Daily : RecurrenceType
    {
        public Daily(int days, DateTime starting, DateTime? endingAfterDate, int? endingAfterNumOfOccurrences) 
            : base(days, starting, endingAfterDate, endingAfterNumOfOccurrences)
        {

        }

        public override IEnumerable<DateTime> GetSchedule(DateTime forecastLimit)
        {
            var occurrenceCount = 0;
            var next = Starting;
            var after = Starting - 1.Days();

            while (true)
            {
                occurrenceCount++;


                if ((EndingAfterDate.HasValue && next > EndingAfterDate.Value) ||
                    (EndingAfterNumOfOccurrences.HasValue && occurrenceCount > EndingAfterNumOfOccurrences) ||
                    next > forecastLimit ||
                    (DateTime.MaxValue.AddDays(-X) - next).Days == 0)
                    yield break;

                if (next > after)
                    yield return next;

                next = next.AddDays(X);

            }
        }
        

        [Obsolete("Try make this obsolete")]
        private bool Next(DateTime after, out DateTime next)
        {
            var occurrenceCount = 1;
            next = Starting;

            if (after < Starting)
                after = Starting - 1.Days();

            while ((next.Ticks - after.Ticks) <= TimeSpan.TicksPerSecond)
            {
                next = next.AddDays(X);
                occurrenceCount++;

                if ((EndingAfterDate.HasValue && next > EndingAfterDate.Value) ||
                    (EndingAfterNumOfOccurrences.HasValue && occurrenceCount > EndingAfterNumOfOccurrences))
                    return false;
            }

            return true;
        }

        [Obsolete("Try make this obsolete")]
        public override DateTime? Prev(DateTime before)
        {
            Next(before, out var next);

            if(before > next)
                before = next;

            while ((before.Ticks - next.Ticks) <= TimeSpan.TicksPerSecond)
                next = next.AddDays(-X);

            if (next < Starting)
                return null;

            return next;
        }
    }
}
