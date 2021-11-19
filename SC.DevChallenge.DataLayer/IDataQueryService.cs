using SC.DevChallenge.DataLayer.Domain;
using System;
using System.Collections.Generic;

namespace SC.DevChallenge.DataLayer
{
    public interface IDataQueryService
    {
        QureyAvgResult GetAvgData(IEnumerable<FinanceInstrument> data, string portfolio, string owner, string instrument, DateTime date);
    }
}