using RoomexEarth.Algorithms.DistanceCalculator;
using RoomexEarth.Logic.Models;

namespace RoomexEarth.Algorithms.Tests.DistanceCalculator
{
    internal class PythagorasDistanceCalculatorTests : BaseDistanceCalculatorTests<PythagorasDistanceCalculator>
    {
        [Test]
        public void Distance_between_known_coordinates_is_known_value()
        {
            /*
             * Independent values taken from 
             * https://www.omnicalculator.com/math/spherical-coordinates
             *   r = 6371   θ = -33   φ = -56
             *   converts to (-1940.3, 2876.7, 5343)
             *   r = 6371   θ = 40   φ = 12
             *   converts to (4006, 851.4, 4880)
             *
             * https://www.omnicalculator.com/math/3d-distance
             *   (-1940.3, 2876.7, 5343) to (4006, 851.4, 4880) = 6,298.79
             */
            var a = new Coordinates(-33, -56);
            var b = new Coordinates(40, 12);
            var distance = _sut.CalculateDistance(a, b);
            Assert.That($"{distance:0}", Is.EqualTo("6299"));
        }
    }
}
