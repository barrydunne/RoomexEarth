using RoomexEarth.Logic.Algorithms;
using RoomexEarth.Logic.Models;

namespace RoomexEarth.Algorithms.DistanceCalculator
{
    /// <inheritdoc/>
    public class CosineDistanceCalculator : IDistanceCalculator
    {
        /// <inheritdoc/>
        public double CalculateDistance(Coordinates locationA, Coordinates locationB)
        {
            if (locationA == locationB)
                return 0;

            var a = (90 - locationB.Latitude).ToRadians();
            var b = (90 - locationA.Latitude).ToRadians();
            var phi = (locationA.Longitude - locationB.Longitude).ToRadians();
            var cosP = (Math.Cos(a) * Math.Cos(b)) + (Math.Sin(a) * Math.Sin(b) * Math.Cos(phi));
            return Constants.EarthRadiusKm * Math.Acos(cosP);
        }
    }
}
