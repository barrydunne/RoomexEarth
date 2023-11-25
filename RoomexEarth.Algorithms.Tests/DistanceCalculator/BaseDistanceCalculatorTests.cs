using RoomexEarth.Logic.Algorithms;
using RoomexEarth.Logic.Models;
using RoomexEarth.Tests.Common;

namespace RoomexEarth.Algorithms.Tests.DistanceCalculator
{
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.Self)]
    [TestFixture(Category = nameof(IDistanceCalculator))]
    internal abstract class BaseDistanceCalculatorTests<T> where T : IDistanceCalculator, new()
    {
        protected readonly T _sut;
        protected readonly CustomFixture _fixture;

        public BaseDistanceCalculatorTests()
        {
            _sut = new();
            _fixture = new();
        }

        [Test]
        public void Distance_between_same_coordinates_is_zero()
        {
            var a = _fixture.Create<Coordinates>();
            var distance = _sut.CalculateDistance(a, a);
            Assert.That(distance, Is.Zero);
        }

        [Test]
        public void Distance_between_two_coordinates_is_greater_than_zero()
        {
            var a = _fixture.Create<Coordinates>();
            var b = _fixture.Create<Coordinates>();
            var distance = _sut.CalculateDistance(a, b);
            Assert.That(distance, Is.GreaterThan(0));
        }

        [Test]
        public void Distance_between_two_coordinates_is_less_than_circumference()
        {
            var a = _fixture.Create<Coordinates>();
            var b = _fixture.Create<Coordinates>();
            var distance = _sut.CalculateDistance(a, b);
            Assert.That(distance, Is.LessThan(2 * Math.PI * 6371));
        }
    }
}
