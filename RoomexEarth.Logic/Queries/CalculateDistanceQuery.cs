using LanguageExt.Common;
using RoomexEarth.Logic.Models;

namespace RoomexEarth.Logic.Queries
{
    /// <summary>
    /// Request the distance between two coordinates.
    /// </summary>
    /// <param name="LocationA">The first location.</param>
    /// <param name="LocationB">The second location.</param>
    public record CalculateDistanceQuery(Coordinates LocationA, Coordinates LocationB) : IQuery<Result<double>>;
}
