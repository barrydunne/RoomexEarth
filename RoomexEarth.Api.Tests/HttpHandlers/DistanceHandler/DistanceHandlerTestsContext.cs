using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using RoomexEarth.Logic.Queries;

namespace RoomexEarth.Api.Tests.HttpHandlers.DistanceHandler
{
    internal class DistanceHandlerTestsContext
    {
        private readonly Fixture _fixture;
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<RoomexEarth.Api.HttpHandlers.DistanceHandler>> _mockLogger;

        private Result<double> _nextResult;

        internal RoomexEarth.Api.HttpHandlers.DistanceHandler Sut { get; }

        public DistanceHandlerTestsContext()
        {
            _fixture = new();
            _mockLogger = new();
            _nextResult = new(_fixture.Create<double>());

            _mockMediator = new(MockBehavior.Strict);
            _mockMediator.Setup(m => m.Send(It.IsAny<CalculateDistanceQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((CalculateDistanceQuery query, CancellationToken _) => GetResult());

            Sut = new(_mockMediator.Object, _mockLogger.Object);
        }

        private Result<double> GetResult()
        {
            var retval = _nextResult;
            _nextResult = new(_fixture.Create<double>());
            return retval;
        }

        internal DistanceHandlerTestsContext WithResult(Result<double> result)
        {
            _nextResult = result;
            return this;
        }
    }
}
