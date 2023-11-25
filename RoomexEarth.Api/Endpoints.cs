using FluentValidation;
using RoomexEarth.Api.HttpHandlers;
using RoomexEarth.Api.Models;
using System.Diagnostics.CodeAnalysis;

namespace RoomexEarth.Api
{
    /// <summary>
    /// Maps the endpoints used in the API.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static class Endpoints
    {
        /// <summary>
        /// Maps the endpoints used in the API.
        /// </summary>
        /// <param name="app">The application to add the routes to.</param>
        internal static void Map(WebApplication app)
        {
            app.MapGet(
                "/health/status",
                () => Results.Ok("GOOD"))
                .WithName("GetHealthStatus")
                .WithDescription("Check API health")
                .WithOpenApi()
                .Produces<string>();

            app.MapGet(
                "/distance/from/{fromLatitude}/{fromLongitude}/to/{toLatitude}/{toLongitude}",
                async (
                    DistanceHandler handler,
                    IValidator<CalculateDistanceRequest> validator,
                    double fromLatitude,
                    double fromLongitude,
                    double toLatitude,
                    double toLongitude)
                    => await handler.CalculateDistanceAsync(new CalculateDistanceRequest(new(fromLatitude, fromLongitude), new(toLatitude, toLongitude)), validator))
                .WithName("CalculateDistance")
                .WithDescription("Calculate the distance in KM between two locations allowing for the curvature of the earth.")
                .WithOpenApi()
                .Produces<CalculateDistanceResponse>();
        }
    }
}
