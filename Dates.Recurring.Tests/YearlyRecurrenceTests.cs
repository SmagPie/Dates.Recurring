using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dates.Recurring.Type;

namespace Dates.Recurring.Tests
{
    public class YearlyRecurrenceTests
    {

        [Fact]
        public void Yearly_EveryYear()
        {
            // Arrange.
            IRecurring yearly = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(1)
                .Years()
                .OnDay(24)
                .OnMonths(Month.JANUARY | Month.FEBRUARY | Month.AUGUST)
                .Ending(new DateTime(2020, 1, 1))
                .Build();

            // Act.

            // Assert.
            Assert.Equal(new DateTime(2015, 1, 24), yearly.Next(new DateTime(2014, 1, 1)));
            Assert.Equal(new DateTime(2015, 1, 24), yearly.Next(new DateTime(2015, 1, 1)));
            Assert.Equal(new DateTime(2015, 1, 24), yearly.Next(new DateTime(2015, 1, 23)));
            Assert.Equal(new DateTime(2015, 2, 24), yearly.Next(new DateTime(2015, 1, 24)));
            Assert.Equal(new DateTime(2015, 2, 24), yearly.Next(new DateTime(2015, 1, 25)));
            Assert.Equal(new DateTime(2015, 2, 24), yearly.Next(new DateTime(2015, 2, 1)));
            Assert.Equal(new DateTime(2015, 2, 24), yearly.Next(new DateTime(2015, 2, 23)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Next(new DateTime(2015, 2, 24)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Next(new DateTime(2015, 3, 24)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Next(new DateTime(2015, 4, 24)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Next(new DateTime(2015, 5, 24)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Next(new DateTime(2015, 6, 24)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Next(new DateTime(2015, 7, 24)));
            Assert.Equal(new DateTime(2016, 1, 24), yearly.Next(new DateTime(2015, 8, 24)));
            Assert.Equal(new DateTime(2016, 2, 24), yearly.Next(new DateTime(2016, 1, 24)));
            Assert.Null(yearly.Next(new DateTime(2020, 1, 1)));

            Assert.Equal(new DateTime(2019, 8, 24), yearly.Prev(new DateTime(2021, 1, 1)));
            Assert.Equal(new DateTime(2015, 1, 24), yearly.Prev(new DateTime(2015, 1, 31)));
            Assert.Equal(new DateTime(2015, 1, 24), yearly.Prev(new DateTime(2015, 1, 25)));
            Assert.Equal(new DateTime(2015, 2, 24), yearly.Prev(new DateTime(2015, 2, 25)));
            Assert.Equal(new DateTime(2015, 1, 24), yearly.Prev(new DateTime(2015, 2, 24)));
            Assert.Equal(new DateTime(2015, 1, 24), yearly.Prev(new DateTime(2015, 2, 23)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Prev(new DateTime(2015, 8, 25)));
            Assert.Equal(new DateTime(2015, 2, 24), yearly.Prev(new DateTime(2015, 8, 24)));
            Assert.Equal(new DateTime(2015, 2, 24), yearly.Prev(new DateTime(2015, 8, 23)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Prev(new DateTime(2016, 1, 1)));
            Assert.Equal(new DateTime(2016, 1, 24), yearly.Prev(new DateTime(2016, 1, 25)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Prev(new DateTime(2016, 1, 24)));
            Assert.Null(yearly.Prev(new DateTime(2015, 1, 24)));
        }

        [Fact]
        public void Yearly_EveryThirdYear()
        {
            // Arrange.
            IRecurring yearly = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(3)
                .Years()
                .OnDay(24)
                .OnMonths(Month.JANUARY | Month.FEBRUARY | Month.AUGUST)
                .Ending(new DateTime(2020, 1, 1))
                .Build();

            // Act.

            // Assert.
            Assert.Equal(new DateTime(2015, 1, 24), yearly.Next(new DateTime(2014, 1, 1)));
            Assert.Equal(new DateTime(2015, 1, 24), yearly.Next(new DateTime(2015, 1, 1)));
            Assert.Equal(new DateTime(2015, 1, 24), yearly.Next(new DateTime(2015, 1, 23)));
            Assert.Equal(new DateTime(2015, 2, 24), yearly.Next(new DateTime(2015, 1, 24)));
            Assert.Equal(new DateTime(2015, 2, 24), yearly.Next(new DateTime(2015, 1, 25)));
            Assert.Equal(new DateTime(2015, 2, 24), yearly.Next(new DateTime(2015, 2, 1)));
            Assert.Equal(new DateTime(2015, 2, 24), yearly.Next(new DateTime(2015, 2, 23)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Next(new DateTime(2015, 2, 24)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Next(new DateTime(2015, 3, 24)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Next(new DateTime(2015, 4, 24)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Next(new DateTime(2015, 5, 24)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Next(new DateTime(2015, 6, 24)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Next(new DateTime(2015, 7, 24)));
            Assert.Equal(new DateTime(2018, 1, 24), yearly.Next(new DateTime(2015, 8, 24)));
            Assert.Equal(new DateTime(2018, 2, 24), yearly.Next(new DateTime(2018, 1, 24)));
            Assert.Equal(new DateTime(2018, 8, 24), yearly.Next(new DateTime(2018, 2, 24)));
            Assert.Equal(new DateTime(2018, 8, 24), yearly.Next(new DateTime(2018, 6, 11)));
            Assert.Null(yearly.Next(new DateTime(2020, 1, 1)));

            Assert.Equal(new DateTime(2018, 8, 24), yearly.Prev(new DateTime(2018, 9, 1)));
            Assert.Equal(new DateTime(2018, 8, 24), yearly.Prev(new DateTime(2018, 8, 25)));
            Assert.Equal(new DateTime(2018, 2, 24), yearly.Prev(new DateTime(2018, 8, 24)));
            Assert.Equal(new DateTime(2018, 2, 24), yearly.Prev(new DateTime(2018, 8, 23)));
            Assert.Equal(new DateTime(2018, 2, 24), yearly.Prev(new DateTime(2018, 2, 25)));
            Assert.Equal(new DateTime(2018, 1, 24), yearly.Prev(new DateTime(2018, 2, 24)));
            Assert.Equal(new DateTime(2018, 1, 24), yearly.Prev(new DateTime(2018, 2, 23)));
            Assert.Equal(new DateTime(2018, 1, 24), yearly.Prev(new DateTime(2018, 1, 25)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Prev(new DateTime(2018, 1, 24)));
            Assert.Equal(new DateTime(2015, 8, 24), yearly.Prev(new DateTime(2015, 8, 25)));
            Assert.Equal(new DateTime(2015, 2, 24), yearly.Prev(new DateTime(2015, 8, 24)));
            Assert.Equal(new DateTime(2015, 2, 24), yearly.Prev(new DateTime(2015, 8, 23)));
            Assert.Equal(new DateTime(2015, 2, 24), yearly.Prev(new DateTime(2015, 2, 25)));
            Assert.Equal(new DateTime(2015, 1, 24), yearly.Prev(new DateTime(2015, 2, 24)));
            Assert.Equal(new DateTime(2015, 1, 24), yearly.Prev(new DateTime(2015, 2, 23)));
            Assert.Equal(new DateTime(2015, 1, 24), yearly.Prev(new DateTime(2015, 1, 25)));
            Assert.Null(yearly.Prev(new DateTime(2015, 1, 24)));
        }

        [Fact]
        public void Yearly_EveryYear_DifferentDaysInMonth()
        {
            // Arrange.
            IRecurring yearly = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(1)
                .Years()
                .OnDay(31)
                .OnMonths(Month.JANUARY | Month.FEBRUARY | Month.AUGUST)
                .Ending(new DateTime(2020, 1, 1))
                .Build();

            // Act.

            // Assert.
            Assert.Equal(new DateTime(2015, 1, 31), yearly.Next(new DateTime(2014, 1, 1)));
            Assert.Equal(new DateTime(2015, 1, 31), yearly.Next(new DateTime(2015, 1, 6)));
            Assert.Equal(new DateTime(2015, 2, 28), yearly.Next(new DateTime(2015, 1, 31)));
            Assert.Equal(new DateTime(2015, 2, 28), yearly.Next(new DateTime(2015, 2, 27)));
            Assert.Equal(new DateTime(2015, 8, 31), yearly.Next(new DateTime(2015, 2, 28)));
            Assert.Null(yearly.Next(new DateTime(2020, 1, 1)));

            Assert.Equal(new DateTime(2019, 8, 31), yearly.Prev(new DateTime(2020, 2, 28)));
            Assert.Equal(new DateTime(2015, 8, 31), yearly.Prev(new DateTime(2016, 1, 1)));
            Assert.Equal(new DateTime(2015, 8, 31), yearly.Prev(new DateTime(2015, 9, 1)));
            Assert.Equal(new DateTime(2015, 2, 28), yearly.Prev(new DateTime(2015, 8, 31)));
            Assert.Equal(new DateTime(2015, 1, 31), yearly.Prev(new DateTime(2015, 2, 28)));
            Assert.Null(yearly.Prev(new DateTime(2015, 1, 31)));
        }

        [Fact]
        public void Yearly_EveryYear_TwoMonthsAfterDateTime()
        {
            // Arrange.
            var startDate = new DateTime(2015, 1, 1);

            IRecurring yearly = Recurs
                .Starting(startDate)
                .Every(1)
                .Years()
                .OnDay(24)
                .OnMonth(startDate.AddMonths(2))
                .Ending(new DateTime(2020, 1, 1))
                .Build();

            // Act.

            // Assert.
            Assert.Equal(new DateTime(2015, 3, 24), yearly.Next(new DateTime(2014, 1, 1)));
            Assert.Equal(new DateTime(2015, 3, 24), yearly.Next(new DateTime(2015, 1, 1)));
            Assert.Equal(new DateTime(2015, 3, 24), yearly.Next(new DateTime(2015, 1, 23)));
            Assert.Equal(new DateTime(2016, 3, 24), yearly.Next(new DateTime(2015, 3, 24)));
            Assert.Equal(new DateTime(2016, 3, 24), yearly.Next(new DateTime(2015, 3, 25)));
            Assert.Equal(new DateTime(2016, 3, 24), yearly.Next(new DateTime(2015, 10, 1)));
            Assert.Equal(new DateTime(2016, 3, 24), yearly.Next(new DateTime(2015, 10, 23)));
            Assert.Equal(new DateTime(2016, 3, 24), yearly.Next(new DateTime(2015, 10, 24)));
            Assert.Equal(new DateTime(2017, 3, 24), yearly.Next(new DateTime(2016, 3, 24)));
            Assert.Equal(new DateTime(2017, 3, 24), yearly.Next(new DateTime(2016, 4, 24)));
            Assert.Null(yearly.Next(new DateTime(2020, 1, 1)));

            Assert.Equal(new DateTime(2019, 3, 24), yearly.Prev(new DateTime(2021, 1, 1)));
            Assert.Equal(new DateTime(2017, 3, 24), yearly.Prev(new DateTime(2018, 2, 24)));
            Assert.Equal(new DateTime(2017, 3, 24), yearly.Prev(new DateTime(2018, 3, 24)));
            Assert.Equal(new DateTime(2016, 3, 24), yearly.Prev(new DateTime(2016, 10, 24)));
            Assert.Equal(new DateTime(2016, 3, 24), yearly.Prev(new DateTime(2016, 10, 23)));
            Assert.Equal(new DateTime(2016, 3, 24), yearly.Prev(new DateTime(2016, 10, 1)));
            Assert.Equal(new DateTime(2016, 3, 24), yearly.Prev(new DateTime(2016, 3, 25)));
            Assert.Equal(new DateTime(2015, 3, 24), yearly.Prev(new DateTime(2016, 3, 24)));
            Assert.Null(yearly.Prev(new DateTime(2015, 3, 24)));
        }

        [Fact]
        public void Yearly_ComparePastAndFutureSeries()
        {
            // Arrange.
            IRecurring recur = Recurs
                .Starting(new DateTime(2015, 1, 1))
                .Every(3)
                .Years()
                .OnDay(24)
                .OnMonths(Month.JANUARY | Month.FEBRUARY | Month.AUGUST)
                .Ending(new DateTime(2040, 1, 1))
                .Build();

            // Act.
            var futureSeries = recur.Future(new DateTime(2014, 1, 1)).Take(5).ToList();
            var last = futureSeries.Last();
            futureSeries.RemoveAt(futureSeries.Count - 1);
            var pastSeries = recur.Past(last).Reverse().ToList();

            Assert.Equal(futureSeries, pastSeries);
        }
    }
}

