using FluentValidation;
using MediatR;
using RoomexEarth.Api.Models;
using RoomexEarth.Logic.Queries;

namespace RoomexEarth.Api.HttpHandlers
{
    /// <summary>
    /// The handler for requests relating to distances.
    /// </summary>
    public class DistanceHandler
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DistanceHandler"/> class.
        /// </summary>
        /// <param name="mediator">The mediator to send commands and queries to.</param>
        /// <param name="logger">The logger to write to.</param>
        public DistanceHandler(IMediator mediator, ILogger<DistanceHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Handle a CalculateDistance request.
        /// </summary>
        /// <param name="request">The request to handle.</param>
        /// <param name="validator">The input validator.</param>
        /// <returns>OK, ValidationProblem or Problem.</returns>
        internal async Task<IResult> CalculateDistanceAsync(CalculateDistanceRequest request, IValidator<CalculateDistanceRequest> validator)
        {
            _logger.LogDebug("{Handler} handler {Request} received.", nameof(DistanceHandler), nameof(CalculateDistanceRequest));

            // Input validation
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            // Implementation
            var result = await _mediator.Send(new CalculateDistanceQuery(request.LocationA, request.LocationB));
            return result.Match(
                success => Results.Ok(new CalculateDistanceResponse(success)),
                error => Results.Problem(error.Message));
        }
    }
}
