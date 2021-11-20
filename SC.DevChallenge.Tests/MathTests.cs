using SC.DevChallenge.DataLayer.Statistics;
using System.Collections.Generic;
using Xunit;

namespace SC.DevChallenge.Tests
{
    public class MathTests
    {
        [Fact]
        public void MedianRetunsCorrectValueForEvenArray()
        {
            var data = new List<decimal> { 2, 5, 7,  11};

            var median = MathStats.GetMedian(data);

            Assert.Equal(6, median);
        }

        [Fact]
        public void MedianRetunsCorrectValueForOddArray()
        {
            var data = new List<decimal> { 2, 5, 11 };

            var median = MathStats.GetMedian(data);

            Assert.Equal(5, median);
        }

        [Fact]
        public void Get1stQuartileRetunsCorrectValueForEvenArray()
        {
            var data = new List<decimal> { 2, 5, 11, 15 };

            var q1 = MathStats.Get1stQuartile(data);

            Assert.Equal(3.5m, q1);
        }

        [Fact]
        public void Get1stQuartileRetunsCorrectValueForOddArray()
        {
            var data = new List<decimal> { 2, 5, 8, 11, 13 };

            var q1 = MathStats.Get1stQuartile(data);

            Assert.Equal(3.5m, q1);
        }

        [Fact]
        public void Get3rdQuartileRetunsCorrectValueForEvenArray()
        {
            var data = new List<decimal> { 2, 5, 11, 15 };

            var q1 = MathStats.Get3rdQuartile(data);

            Assert.Equal(13m, q1);
        }

        [Fact]
        public void Get3rdQuartileRetunsCorrectValueForOddArray()
        {
            var data = new List<decimal> { 2, 5, 8, 11, 13 };

            var q1 = MathStats.Get3rdQuartile(data);

            Assert.Equal(12m, q1);
        }
    }
}
