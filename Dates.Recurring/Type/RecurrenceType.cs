using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dates.Recurring.Type
{
    public abstract class RecurrenceType : IRecurring
    {
        protected int X { get; set; }
        protected DateTime Starting { get; set; }
        protected DateTime? EndingAfterDate { get; set; }
        protected int? EndingAfterNumOfOccurrences { get; set; }

        protected RecurrenceType(int x, DateTime starting, DateTime? endingAfterDate = null, int? endingAfterNumOfOccurrences = null)
        {
            X = x;
            Starting = starting;
            EndingAfterDate = endingAfterDate;
            EndingAfterNumOfOccurrences = endingAfterNumOfOccurrences;
        }

        public abstract DateTime? Next(DateTime after);
        public abstract DateTime? Prev(DateTime before);

        public virtual IEnumerable<DateTime> Past(DateTime before)
        {
            for (var prev = Prev(before); prev != null; prev = Prev(prev.Value))
                yield return prev.Value;
        }

        public virtual IEnumerable<DateTime> Future(DateTime after)
        {
            for (var next = Next(after); next != null; next = Next(next.Value))
                yield return next.Value;
        }
    }
}
