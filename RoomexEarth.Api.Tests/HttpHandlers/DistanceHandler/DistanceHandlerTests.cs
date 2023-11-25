using LanguageExt.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using RoomexEarth.Api.Models;
using RoomexEarth.Api.Validators;
using RoomexEarth.Logic.Models;
using RoomexEarth.Tests.Common;

namespace RoomexEarth.Api.Tests.HttpHandlers.DistanceHandler
{
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.Self)]
    [TestFixture(Category = "HttpHandlers")]
    public class DistanceHandlerTests
    {
        private readonly CustomFixture _fixture;
        private readonly DistanceHandlerTestsContext _context;

        public DistanceHandlerTests()
        {
            _fixture = new();
            _context = new();
        }

        [Test]
        public async Task DistanceHandler_returns_ProblemHttpResult_for_invalid_input()
        {
            var request = _fixture.Build<CalculateDistanceRequest>()
                                  .With(_ => _.LocationA, new Coordinates(-100, 0))
                                  .Create();

            var response = await _context.Sut.CalculateDistanceAsync(request, new CalculateDistanceRequestValidator());

            Assert.That(response, Is.TypeOf<ProblemHttpResult>());
        }

        [Test]
        public async Task DistanceHandler_returns_status_400_for_invalid_input()
        {
            var request = _fixture.Build<CalculateDistanceRequest>()
                                  .With(_ => _.LocationA, new Coordinates(-100, 0))
                                  .Create();

            var response = await _context.Sut.CalculateDistanceAsync(request, new CalculateDistanceRequestValidator());

            Assert.That((response as ProblemHttpResult)?.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task DistanceHandler_returns_error_details_for_invalid_input()
        {
            var request = _fixture.Build<CalculateDistanceRequest>()
                                  .With(_ => _.LocationA, new Coordinates(-100, 0))
                                  .Create();

            var response = await _context.Sut.CalculateDistanceAsync(request, new CalculateDistanceRequestValidator());

            var error = ((response as ProblemHttpResult)?.ProblemDetails as Microsoft.AspNetCore.Http.HttpValidationProblemDetails)?.Errors?.FirstOrDefault(_ => _.Key == "LocationA.Latitude");
            Assert.That(error?.Value, Is.EquivalentTo(new[] { "'Location A Latitude' must be greater than or equal to '-90'." }));
        }

        [Test]
        public async Task DistanceHandler_returns_OK_on_success()
        {
            var request = _fixture.Create<CalculateDistanceRequest>();
            var result = new Result<double>(_fixture.Create<double>());

            var response = await _context.WithResult(result)
                                         .Sut.CalculateDistanceAsync(request, new CalculateDistanceRequestValidator());

            Assert.That(response, Is.TypeOf<Ok<CalculateDistanceResponse>>());
        }

        [Test]
        public async Task DistanceHandler_returns_query_handler_value_on_success()
        {
            var request = _fixture.Create<CalculateDistanceRequest>();
            var km = _fixture.Create<double>();
            var result = new Result<double>(km);

            var response = await _context.WithResult(result)
                                         .Sut.CalculateDistanceAsync(request, new CalculateDistanceRequestValidator());

            Assert.That((response as Ok<CalculateDistanceResponse>)?.Value?.DistanceKm, Is.EqualTo(km));
        }

        [Test]
        public async Task DistanceHandler_returns_query_handler_value_in_miles_on_success()
        {
            var request = _fixture.Create<CalculateDistanceRequest>();
            var km = _fixture.Create<double>();
            var miles = 0.62137119 * km;
            var result = new Result<double>(km);

            var response = await _context.WithResult(result)
                                         .Sut.CalculateDistanceAsync(request, new CalculateDistanceRequestValidator());

            Assert.That((response as Ok<CalculateDistanceResponse>)?.Value?.DistanceMiles.ToString("0.0000"), Is.EqualTo(miles.ToString("0.0000")));
        }

        [Test]
        public async Task DistanceHandler_returns_Problem_on_error()
        {
            var request = _fixture.Create<CalculateDistanceRequest>();
            var exception = new InvalidOperationException(_fixture.Create<string>());
            var result = new Result<double>(exception);

            var response = await _context.WithResult(result)
                                         .Sut.CalculateDistanceAsync(request, new CalculateDistanceRequestValidator());

            Assert.That(response, Is.TypeOf<ProblemHttpResult>());
        }

        [Test]
        public async Task DistanceHandler_returns_status_500_on_error()
        {
            var request = _fixture.Create<CalculateDistanceRequest>();
            var exception = new InvalidOperationException(_fixture.Create<string>());
            var result = new Result<double>(exception);

            var response = await _context.WithResult(result)
                                         .Sut.CalculateDistanceAsync(request, new CalculateDistanceRequestValidator());

            Assert.That((response as ProblemHttpResult)?.StatusCode, Is.EqualTo(500));
        }

        [Test]
        public async Task DistanceHandler_returns_error_details_value_on_error()
        {
            var request = _fixture.Create<CalculateDistanceRequest>();
            var exception = new InvalidOperationException(_fixture.Create<string>());
            var result = new Result<double>(exception);

            var response = await _context.WithResult(result)
                                         .Sut.CalculateDistanceAsync(request, new CalculateDistanceRequestValidator());

            Assert.That((response as ProblemHttpResult)?.ProblemDetails?.Detail, Is.EqualTo(exception.Message));
        }
    }
}
