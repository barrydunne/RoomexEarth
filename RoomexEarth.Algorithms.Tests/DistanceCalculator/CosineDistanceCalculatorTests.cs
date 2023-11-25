using RoomexEarth.Algorithms.DistanceCalculator;
using RoomexEarth.Logic.Models;

namespace RoomexEarth.Algorithms.Tests.DistanceCalculator
{
    internal class CosineDistanceCalculatorTests : BaseDistanceCalculatorTests<CosineDistanceCalculator>
    {
        [Test]
        public void Distance_between_known_coordinates_is_known_value()
        {
            /*
             * Independent values taken from https://www.themathdoctors.org/distances-on-earth-1-the-cosine-formula/
             * 1.680433715 * 6371 = 10706.043198265
             */
            var a = new Coordinates(-33, -56);
            var b = new Coordinates(40, 12);
            var distance = _sut.CalculateDistance(a, b);
            Assert.That($"{distance:0.000000}", Is.EqualTo("10706.043195"));
        }
    }
}
