using SC.DevChallenge.DataLayer.Helpers;
using System;
using Xunit;

namespace SC.DevChallenge.Tests
{
    public class TsToDate
    {
        [Fact]
        public void TimeIntervalService_returns_correct_date_for_timeslot_number_0()
        {
            var slot = 0;
            var date = TimeIntervalService.TimeSlotToDate(slot);

            Assert.Equal(new DateTime(2018, 1, 1), date);
        }

        [Fact]
        public void TimeIntervalService_returns_correct_date_for_timeslot_number_1()
        {
            var slot = 1;
            var date = TimeIntervalService.TimeSlotToDate(slot);

            Assert.Equal(new DateTime(2018, 1, 1).AddSeconds(10000), date);
        }


        [Fact]
        public void TimeIntervalService_returns_correct_date_for_timeslot_number_2()
        {
            var slot = 1;
            var date = TimeIntervalService.TimeSlotToDate(slot);

            Assert.NotEqual(new DateTime(2018, 1, 1).AddSeconds(10001), date);
        }
    }
}
