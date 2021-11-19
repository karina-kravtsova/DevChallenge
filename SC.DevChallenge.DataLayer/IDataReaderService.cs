using SC.DevChallenge.DataLayer.Domain;
using System.Collections.Generic;

namespace SC.DevChallenge.DataLayer
{
    public interface IDataReaderService
    {
        IEnumerable<FinanceInstrument> GetAll(string path, string dateFormat);
    }
}