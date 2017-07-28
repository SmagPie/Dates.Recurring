using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dates.Recurring.Type
{
    public interface IRecurring
    {
        DateTime? Next(DateTime after);
        DateTime? Prev(DateTime before);
        IEnumerable<DateTime> Past(DateTime before);
        IEnumerable<DateTime> Future(DateTime after);
    }
}
