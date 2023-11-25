using RoomexEarth.Logic.Models;

namespace RoomexEarth.Api.Models
{
    /// <summary>
    /// Request the distance between two coordinates.
    /// </summary>
    /// <param name="LocationA">The first location.</param>
    /// <param name="LocationB">The second location.</param>
    public record CalculateDistanceRequest(Coordinates LocationA, Coordinates LocationB);
}
