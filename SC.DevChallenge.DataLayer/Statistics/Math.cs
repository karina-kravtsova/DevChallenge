using System.Collections.Generic;
using System.Linq;

namespace SC.DevChallenge.DataLayer.Statistics
{
    public class MathStats
    {
        public static decimal GetMedian(IEnumerable<decimal> input)
        {
            var data = input.OrderBy(d => d);

            var length = data.Count();
            
            // for even count
            if (length % 2 == 0)
            {
                length++;

                // reduce index by 1
                var middle = length / 2 - 1;  
                
                // take average of 2 middle elements
                var median = (data.ElementAt(middle) + data.ElementAt(middle + 1)) / 2;
                return median;
            }
            // for odd count
            else
            {
                length++;

                // take middle element
                var middle = length / 2 - 1;
                return data.ElementAt(middle);
            }
        }

        public static decimal Get1stQuartile(IEnumerable<decimal> input)
        {
            var median = GetMedian(input);

            // lower quartile - everything less median
            var lowerQuartile = input.Where(c => c < median);

            var q1 = GetMedian(lowerQuartile);

            return q1;
        }

        public static decimal Get3rdQuartile(IEnumerable<decimal> input)
        {
            var median = GetMedian(input);

            // lower quartile - everything more median
            var higherQuartile = input.Where(c => c > median);

            var q1 = GetMedian(higherQuartile);

            return q1;
        }

        public static IEnumerable<decimal> RemoveOutliers(IEnumerable<decimal> input)
        {
            var q1 = Get1stQuartile(input);
            var q3 = Get3rdQuartile(input);
            var iqr = q3 - q1;

            var lowerBound = q1 - 1.5m * iqr;
            var upperBound = q3 + 1.5m * iqr;

            return input.Where(i => i >= lowerBound && i <= upperBound);
        }
    }
}
