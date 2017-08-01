using Dates.Recurring.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dates.Recurring.Builders
{
    public class DaysBuilder
    {
        private int _days;
        private DateTime _starting;
        private DateTime? _endingAfterDate;
        private int? _endingAfterNumOfOccurrences;

        public DaysBuilder(int days, DateTime starting)
        {
            _days = days;
            _starting = starting;
        }

        public DaysBuilder Ending(DateTime afterDate)
        {
            _endingAfterDate = afterDate;
            return this;
        }

        public DaysBuilder Ending(int afterNumOfRecurrences)
        {
            _endingAfterNumOfOccurrences = afterNumOfRecurrences;
            return this;
        }

        public Daily Build()
        {
            return new Daily(_days, _starting, _endingAfterDate, _endingAfterNumOfOccurrences);
        }
    }
}
