using Dates.Recurring.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dates.Recurring.Tests
{
    
    public class DailyRecurrenceTests
    {
        [Fact]
        public void Daily_EveryDay()
        {
            // Arrange.
            IRecurring daily = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(1)
                .Days()
                .Ending(new DateTime(2015, 1, 15))
                .Build();

            // Act.

            // Assert.
            Assert.Equal(new DateTime(2015, 1, 1), daily.Next(new DateTime(2014, 7, 3)));
            Assert.Equal(new DateTime(2015, 1, 2), daily.Next(new DateTime(2015, 1, 1)));
            Assert.Equal(new DateTime(2015, 1, 3), daily.Next(new DateTime(2015, 1, 2)));
            Assert.Null(daily.Next(new DateTime(2015, 1, 15)));
            Assert.Equal(new DateTime(2015, 1, 15), daily.Prev(new DateTime(2016, 7, 3)));
            Assert.Equal(new DateTime(2015, 1, 14), daily.Prev(new DateTime(2015, 1, 15)));
            Assert.Equal(new DateTime(2015, 1, 13), daily.Prev(new DateTime(2015, 1, 14)));
            Assert.Null(daily.Prev(new DateTime(2015, 1, 1)));
        }

        [Fact]
        public void Daily_EveryThirdDay()
        {
            // Arrange.
            IRecurring daily = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(3)
                .Days()
                .Ending(new DateTime(2015, 1, 15))
                .Build();

            // Act.

            // Assert.
            Assert.Equal(new DateTime(2015, 1, 1), daily.Next(new DateTime(2014, 7, 3)));
            Assert.Equal(new DateTime(2015, 1, 4), daily.Next(new DateTime(2015, 1, 1)));
            Assert.Equal(new DateTime(2015, 1, 7), daily.Next(new DateTime(2015, 1, 5)));
            Assert.Equal(new DateTime(2015, 1, 13), daily.Next(new DateTime(2015, 1, 10)));
            Assert.Equal(new DateTime(2015, 1, 13), daily.Prev(new DateTime(2016, 7, 3)));
            Assert.Equal(new DateTime(2015, 1, 1), daily.Prev(new DateTime(2015, 1, 4)));
            Assert.Equal(new DateTime(2015, 1, 13), daily.Prev(new DateTime(2015, 1, 15)));
            Assert.Equal(new DateTime(2015, 1, 10), daily.Prev(new DateTime(2015, 1, 11)));
        }

        [Fact]
        public void Daily_ComparePastAndFutureSeries()
        {
            // Arrange.
            IRecurring recur = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(3)
                .Days()
                .Ending(new DateTime(2015, 1, 30))
                .Build();

            // Act.
            var futureSeries = recur.Future(new DateTime(2014, 1, 1)).Take(5).ToList();
            var last = futureSeries.Last();
            futureSeries.RemoveAt(futureSeries.Count - 1);
            var pastSeries = recur.Past(last).Reverse().ToList();

            Assert.Equal(futureSeries, pastSeries);
        }

        [Fact]
        public void Daily_EndAfterNumOfOccurences()
        {
            IRecurring daily = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(1)
                .Days()
                .Ending(2)
                .Build();

            Assert.Equal(new DateTime(2015, 1, 1), daily.Next(new DateTime(2014, 7, 3)));
            Assert.Equal(new DateTime(2015, 1, 2), daily.Next(new DateTime(2015, 1, 1)));
            Assert.Null(daily.Next(new DateTime(2015, 1, 3)));

            Assert.Equal(new DateTime(2015, 1, 2), daily.Prev(new DateTime(2017, 1, 1)));
            Assert.Equal(new DateTime(2015, 1, 1), daily.Prev(new DateTime(2015, 1, 2)));
            Assert.Null(daily.Prev(new DateTime(2015, 1, 1)));
        }
    }
}
