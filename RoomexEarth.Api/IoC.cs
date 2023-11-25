using FluentValidation;
using RoomexEarth.Algorithms.DistanceCalculator;
using RoomexEarth.Api.HttpHandlers;
using RoomexEarth.Api.Validators;
using RoomexEarth.Logic.Algorithms;
using RoomexEarth.Logic.Queries;
using System.Diagnostics.CodeAnalysis;

namespace RoomexEarth.Api
{
    /// <summary>
    /// Register services for dependency injection.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static class IoC
    {
        /// <summary>
        /// Register services for dependency injection.
        /// </summary>
        /// <param name="builder">The builder to register services with.</param>
        internal static void RegisterServices(WebApplicationBuilder builder)
        {
            // API Handlers
            builder.Services
                .AddTransient<DistanceHandler>();

            // Validators
            builder.Services
                .AddValidatorsFromAssemblyContaining<CalculateDistanceRequestValidator>();

            // CQRS
            builder.Services
                .AddMediatR(_ => _.RegisterServicesFromAssembly(typeof(CalculateDistanceQuery).Assembly));

            // Implementations
            builder.Services
                .AddTransient<IDistanceCalculator, CosineDistanceCalculator>();
        }
    }
}
