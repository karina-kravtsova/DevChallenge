using CsvHelper.Configuration.Attributes;
using System;

namespace SC.DevChallenge.DataLayer.Domain
{
    public class FinanceInstrument
    {
        public string Portfolio { get; set; }
        public string Owner { get; set; }
        public string Instrument { get; set; }
        public DateTime Date { get; set; }
        [Ignore]
        public int TimeSlot { get; set; }
        public decimal Price { get; set; }
    }
}
