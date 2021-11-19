using CsvHelper;
using CsvHelper.TypeConversion;
using SC.DevChallenge.DataLayer.Domain;
using SC.DevChallenge.DataLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SC.DevChallenge.DataLayer
{
    public class CsvReaderService : IDataReaderService
    {
        public IEnumerable<FinanceInstrument> GetAll(string csvPath, string dateFormat)
        {
            IEnumerable<FinanceInstrument> records;

            using (var reader = new StreamReader(csvPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var options = new TypeConverterOptions { Formats = new[] { dateFormat } };
                csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);
        
                records = csv.GetRecords<FinanceInstrument>().ToList();
            }

            records = records.Select(d => {
                d.TimeSlot = TimeIntervalService.DateToTimeSlot(d.Date);
                return d;
            });

            return records;
        }
    }
}
