using System;

namespace SC.DevChallenge.DataLayer.Helpers
{
    public class TimeIntervalService
    {
        private static readonly DateTime start = new(2018, 1, 1);
        private const int intervalSeconds = 10000;

        public static int DateToTimeSlot(DateTime date)
        {
            return (int)Math.Floor((date - start).TotalSeconds / intervalSeconds);
        }

        public static DateTime TimeSlotToDate(int slotNumber)
        {
            return start.AddSeconds(slotNumber * intervalSeconds);
        }

    }
}
