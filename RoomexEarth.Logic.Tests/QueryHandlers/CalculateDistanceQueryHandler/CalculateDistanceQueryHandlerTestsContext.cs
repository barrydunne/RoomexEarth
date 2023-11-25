using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using RoomexEarth.Logic.Algorithms;
using RoomexEarth.Logic.Models;

namespace RoomexEarth.Logic.Tests.QueryHandlers.CalculateDistanceQueryHandler
{
    internal class CalculateDistanceQueryHandlerTestsContext
    {
        private readonly Fixture _fixture;
        private readonly Mock<IDistanceCalculator> _mockDistanceCalculator;
        private readonly Mock<ILogger<RoomexEarth.Logic.QueryHandlers.CalculateDistanceQueryHandler>> _mockLogger;

        private Exception? _nextException;
        private double _nextDistance;

        internal RoomexEarth.Logic.QueryHandlers.CalculateDistanceQueryHandler Sut { get; }

        public CalculateDistanceQueryHandlerTestsContext()
        {
            _fixture = new();
            _mockLogger = new();
            _nextDistance = _fixture.Create<double>();

            _mockDistanceCalculator = new(MockBehavior.Strict);
            _mockDistanceCalculator.Setup(_ => _.CalculateDistance(It.IsAny<Coordinates>(), It.IsAny<Coordinates>()))
                .Returns(() => GetDistance());

            Sut = new(_mockDistanceCalculator.Object, _mockLogger.Object);
        }

        private double GetDistance()
        {
            if (_nextException is not null)
            {
                var ex = _nextException;
                _nextException = null;
                throw ex;
            }

            var retval = _nextDistance;
            _nextDistance = _fixture.Create<double>();
            return retval;
        }

        internal CalculateDistanceQueryHandlerTestsContext WithDistance(double distance)
        {
            _nextDistance = distance;
            return this;
        }

        internal CalculateDistanceQueryHandlerTestsContext WithException(InvalidOperationException exception)
        {
            _nextException = exception;
            return this;
        }
    }
}
