using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dates.Recurring.Type;

namespace Dates.Recurring.Tests
{
    public class WeeklyRecurrenceTests
    {
        [Fact]
        public void Weekly_EveryWeek()
        {
            // Arrange.
            IRecurring weekly = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(1)
                .Weeks()
                .OnDays(Day.TUESDAY | Day.FRIDAY)
                .Ending(new DateTime(2015, 2, 19))
                .Build();

            // Act.

            // Assert.
            Assert.Equal(new DateTime(2015, 1, 2), weekly.Next(new DateTime(2014, 1, 1)));
            Assert.Equal(new DateTime(2015, 1, 6), weekly.Next(new DateTime(2015, 1, 2)));
            Assert.Equal(new DateTime(2015, 1, 9), weekly.Next(new DateTime(2015, 1, 6)));
            Assert.Equal(new DateTime(2015, 1, 13), weekly.Next(new DateTime(2015, 1, 9)));
            Assert.Equal(new DateTime(2015, 1, 16), weekly.Next(new DateTime(2015, 1, 13)));

            Assert.Equal(new DateTime(2015, 2, 17), weekly.Prev(new DateTime(2016, 1, 3)));
            Assert.Equal(new DateTime(2015, 2, 13), weekly.Prev(new DateTime(2015, 2, 17)));
            Assert.Equal(new DateTime(2015, 2, 13), weekly.Prev(new DateTime(2015, 2, 14)));
            Assert.Equal(new DateTime(2015, 2, 10), weekly.Prev(new DateTime(2015, 2, 13)));
            Assert.Equal(new DateTime(2015, 2, 10), weekly.Prev(new DateTime(2015, 2, 12)));
            Assert.Null(weekly.Prev(new DateTime(2015, 1, 2)));
        }

        [Fact]
        public void Weekly_EveryThirdWeek()
        {
            // Arrange.
            IRecurring weekly = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(3)
                .Weeks()
                .OnDays(Day.TUESDAY | Day.FRIDAY)
                .Ending(new DateTime(2015, 2, 19))
                .Build();

            // Act.

            // Assert.
            Assert.Equal(new DateTime(2015, 1, 2), weekly.Next(new DateTime(2014, 1, 1)));
            Assert.Equal(new DateTime(2015, 1, 20), weekly.Next(new DateTime(2015, 1, 2)));
            Assert.Equal(new DateTime(2015, 1, 23), weekly.Next(new DateTime(2015, 1, 21)));
            Assert.Equal(new DateTime(2015, 2, 10), weekly.Next(new DateTime(2015, 1, 23)));
            Assert.Equal(new DateTime(2015, 2, 10), weekly.Next(new DateTime(2015, 1, 24)));
            Assert.Equal(new DateTime(2015, 2, 10), weekly.Next(new DateTime(2015, 1, 27)));
            Assert.Equal(new DateTime(2015, 2, 13), weekly.Next(new DateTime(2015, 2, 10)));
            Assert.Null(weekly.Next(new DateTime(2015, 2, 13)));

            Assert.Equal(new DateTime(2015, 2, 13), weekly.Prev(new DateTime(2017, 1, 1)));
            Assert.Equal(new DateTime(2015, 2, 10), weekly.Prev(new DateTime(2015, 2, 13)));
            Assert.Equal(new DateTime(2015, 2, 10), weekly.Prev(new DateTime(2015, 2, 11)));
            Assert.Equal(new DateTime(2015, 1, 23), weekly.Prev(new DateTime(2015, 2, 10)));
            Assert.Equal(new DateTime(2015, 1, 23), weekly.Prev(new DateTime(2015, 1, 31)));
            Assert.Equal(new DateTime(2015, 1, 20), weekly.Prev(new DateTime(2015, 1, 22)));
            Assert.Equal(new DateTime(2015, 1, 2), weekly.Prev(new DateTime(2015, 1, 3)));
            Assert.Null(weekly.Prev(new DateTime(2015, 1, 2)));
        }

        [Fact]
        public void Weekly_EveryWeek_TwoDaysAfterDateTime()
        {
            // Arrange.
            DateTime startDate = new DateTime(2015, 1, 1);

            IRecurring weekly = Recurs
                .Starting(startDate)
                .Every(1)
                .Weeks()
                .OnDay(DayOfWeek.Saturday)
                .Ending(new DateTime(2015, 2, 19))
                .Build();

            // Act.

            // Assert.
            Assert.Equal(new DateTime(2015, 1, 3), weekly.Next(new DateTime(2014, 1, 1)));
            Assert.Equal(new DateTime(2015, 1, 3), weekly.Next(new DateTime(2015, 1, 2)));
            Assert.Equal(new DateTime(2015, 1, 10), weekly.Next(new DateTime(2015, 1, 6)));
            Assert.Equal(new DateTime(2015, 1, 10), weekly.Next(new DateTime(2015, 1, 9)));
            Assert.Equal(new DateTime(2015, 1, 17), weekly.Next(new DateTime(2015, 1, 13)));
            Assert.Null(weekly.Next(new DateTime(2015, 2, 19)));

            Assert.Equal(new DateTime(2015, 2, 14), weekly.Prev(new DateTime(2016, 1, 1)));
            Assert.Equal(new DateTime(2015, 2, 7), weekly.Prev(new DateTime(2015, 2, 14)));
            Assert.Equal(new DateTime(2015, 2, 7), weekly.Prev(new DateTime(2015, 2, 8)));
            Assert.Equal(new DateTime(2015, 1, 31), weekly.Prev(new DateTime(2015, 2, 7)));
            Assert.Equal(new DateTime(2015, 1, 31), weekly.Prev(new DateTime(2015, 2, 6)));
            Assert.Null(weekly.Prev(new DateTime(2015, 1, 3)));
        }

        [Fact]
        public void Weekly_ComparePastAndFutureSeries()
        {
            // Arrange.
            IRecurring recur = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(3)
                .Weeks()
                .OnDays(Day.TUESDAY | Day.FRIDAY)
                .Ending(new DateTime(2015, 2, 19))
                .Build();

            // Act.
            var futureSeries = recur.Future(new DateTime(2014, 1, 1)).Take(5).ToList();
            var last = futureSeries.Last();
            futureSeries.RemoveAt(futureSeries.Count - 1);
            var pastSeries = recur.Past(last).Reverse().ToList();

            Assert.Equal(futureSeries, pastSeries);
        }

        [Fact]
        public void Weekly_EndAfterNumOfOccurences()
        {
            IRecurring weekly = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(3)
                .Weeks()
                .OnDays(Day.TUESDAY | Day.FRIDAY)
                .Ending(2)
                .Build();

            Assert.Equal(new DateTime(2015, 1, 2), weekly.Next(new DateTime(2014, 7, 3)));
            Assert.Equal(new DateTime(2015, 1, 20), weekly.Next(new DateTime(2015, 1, 2)));
            Assert.Null(weekly.Next(new DateTime(2015, 1, 20)));

            Assert.Equal(new DateTime(2015, 1, 20), weekly.Prev(new DateTime(2017, 1, 1)));
            Assert.Equal(new DateTime(2015, 1, 2), weekly.Prev(new DateTime(2015, 1, 20)));
            Assert.Null(weekly.Prev(new DateTime(2015, 1, 2)));
        }
    }
}
