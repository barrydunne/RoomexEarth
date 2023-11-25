using AutoFixture;
using RoomexEarth.Logic.Queries;
using RoomexEarth.Tests.Common;

namespace RoomexEarth.Logic.Tests.QueryHandlers.CalculateDistanceQueryHandler
{
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.Self)]
    [TestFixture(Category = "QueryHandlers")]
    internal class CalculateDistanceQueryHandlerTests
    {
        private readonly CustomFixture _fixture;
        private readonly CalculateDistanceQueryHandlerTestsContext _context;

        public CalculateDistanceQueryHandlerTests()
        {
            _fixture = new();
            _context = new();
        }

        [Test]
        public async Task CalculateDistanceQueryHandler_returns_success_when_no_exception()
        {
            var query = _fixture.Create<CalculateDistanceQuery>();

            var result = await _context.Sut.Handle(query, CancellationToken.None);

            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        public async Task CalculateDistanceQueryHandler_returns_distance_from_algorithm()
        {
            var distance = _fixture.Create<double>();
            var query = _fixture.Create<CalculateDistanceQuery>();
            
            var result = await _context.WithDistance(distance)
                                       .Sut.Handle(query, CancellationToken.None);
            
            Assert.That(result.Equals(distance), Is.True);
        }

        [Test]
        public async Task CalculateDistanceQueryHandler_returns_faulted_when_exception()
        {
            var exception = new InvalidOperationException(_fixture.Create<string>());
            var query = _fixture.Create<CalculateDistanceQuery>();

            var result = await _context.WithException(exception).Sut.Handle(query, CancellationToken.None);

            Assert.That(result.IsFaulted, Is.True);
        }

        [Test]
        public async Task CalculateDistanceQueryHandler_returns_exception_when_faulted()
        {
            var exception = new InvalidOperationException(_fixture.Create<string>());
            var query = _fixture.Create<CalculateDistanceQuery>();
            Exception? thrown = null;

            var result = await _context.WithException(exception).Sut.Handle(query, CancellationToken.None);

            result.IfFail((_) => thrown = _);
            Assert.That(thrown, Is.EqualTo(exception));
        }
    }
}
