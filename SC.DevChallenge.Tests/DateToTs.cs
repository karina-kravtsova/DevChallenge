using SC.DevChallenge.DataLayer.Helpers;
using System;
using Xunit;

namespace SC.DevChallenge.Tests
{
    public class DateToTs
    {
        [Fact]
        public void TimeIntervalService_returns_correct_timeslot_number_0()
        {
            var date = new DateTime(2018, 1, 1);
            date.AddSeconds(1);

            var slot = TimeIntervalService.DateToTimeSlot(date);

            Assert.Equal(0, slot);
        }

        [Fact]
        public void TimeIntervalService_returns_correct_timeslot_number_0_()
        {
            var date = new DateTime(2018, 1, 1);
            date = date.AddSeconds(9999);

            var slot = TimeIntervalService.DateToTimeSlot(date);

            Assert.Equal(0, slot);
        }

        [Fact]
        public void TimeIntervalService_returns_correct_timeslot_number_1()
        {
            var date = new DateTime(2018, 1, 1);
            date = date.AddSeconds(10000);

            var slot = TimeIntervalService.DateToTimeSlot(date);

            Assert.Equal(1, slot);
        }
    }
}
