using SC.DevChallenge.DataLayer.Domain;
using SC.DevChallenge.DataLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SC.DevChallenge.DataLayer
{
    public class DataQueryService : IDataQueryService
    {
        public QureyAvgResult GetAvgData(IEnumerable<FinanceInstrument> data, string portfolio, string owner, string instrument, DateTime date)
        {
            var timeSlot = TimeIntervalService.DateToTimeSlot(date);

            var filteredData = data.Where(d =>
                (string.IsNullOrEmpty(portfolio) || d.Portfolio == portfolio)
                && (string.IsNullOrEmpty(owner) || d.Owner == owner)
                && (string.IsNullOrEmpty(instrument) || d.Instrument == instrument)
                && d.TimeSlot == timeSlot);

            if (!filteredData.Any())
            {
                return null;
            }

            var result = new QureyAvgResult
            {
                TimeSlotDate = TimeIntervalService.TimeSlotToDate(timeSlot),
                AvgPrice = filteredData.Average(d => d.Price)
            };

            return result;
        }
    }
}
