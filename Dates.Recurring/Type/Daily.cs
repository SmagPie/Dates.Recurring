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
        public Daily(int days, DateTime starting, DateTime? ending) : base(days, starting, ending)
        {
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

            while ((next.Ticks - after.Ticks) <= TimeSpan.TicksPerSecond)
            {
                next = next.AddDays(X);
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

            while ((before.Ticks - next.Ticks) <= TimeSpan.TicksPerSecond)
            {
                next = next.AddDays(-X);
            }

            if (next.Date < Starting.Date)
            {
                return null;
            }

            return next;
        }
    }
}
