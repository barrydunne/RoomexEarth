using RoomexEarth.Logic.Models;

namespace RoomexEarth.Logic.Algorithms
{
    /// <summary>
    /// Provides functionality for calculating distances.
    /// </summary>
    public interface IDistanceCalculator
    {
        /// <summary>
        /// Calculate the distance in KM between two locations on the surface of the earth.
        /// </summary>
        /// <param name="locationA">The first location.</param>
        /// <param name="locationB">The second location.</param>
        /// <returns>The distance in KM between the two locations on the surface of the earth.</returns>
        double CalculateDistance(Coordinates locationA, Coordinates locationB);
    }
}
