using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using RoomexEarth.Logic.Algorithms;
using RoomexEarth.Logic.Queries;

namespace RoomexEarth.Logic.QueryHandlers
{
    /// <summary>
    /// The handler for the <see cref="CalculateDistanceQuery"/> query.
    /// </summary>
    internal class CalculateDistanceQueryHandler : IQueryHandler<CalculateDistanceQuery, Result<double>>
    {
        private readonly IDistanceCalculator _distanceCalculator;
        private readonly ILogger<CalculateDistanceQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculateDistanceQueryHandler"/> class.
        /// </summary>
        /// <param name="distanceCalculator">The distance calculator implementation.</param>
        /// <param name="logger">The logger to write to.</param>
        public CalculateDistanceQueryHandler(IDistanceCalculator distanceCalculator, ILogger<CalculateDistanceQueryHandler> logger)
        {
            _distanceCalculator = distanceCalculator;
            _logger = logger;
        }

        /// <inheritdoc/>
        public Task<Result<double>> Handle(CalculateDistanceQuery request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("{Handler} handler.", nameof(CalculateDistanceQuery));
            Result<double> result;
            try
            {
                var distance = _distanceCalculator.CalculateDistance(request.LocationA, request.LocationB);
                _logger.LogInformation("Distance between {LocationA} and {LocationB} is {Distance}.", request.LocationA, request.LocationB, distance);
                result = new(distance);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to calculate distance between {LocationA} and {LocationB}", request.LocationA, request.LocationB);
                result = new(ex);
            }
            return Task.FromResult(result);
        }
    }
}
