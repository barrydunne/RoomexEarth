// StyleCop is not yet handling C# 12 Alias types - https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-12.0/using-alias-types
#pragma warning disable SA1008 // Opening parenthesis should be spaced correctly

using RoomexEarth.Logic.Algorithms;
using RoomexEarth.Logic.Models;
using Cartesian = (double x, double y, double z);

#pragma warning restore SA1008

namespace RoomexEarth.Algorithms.DistanceCalculator
{
    /// <inheritdoc/>
    public class PythagorasDistanceCalculator : IDistanceCalculator
    {
        /// <inheritdoc/>
        public double CalculateDistance(Coordinates locationA, Coordinates locationB)
        {
            if (locationA == locationB)
                return 0;

            var (xA, yA, zA) = GetCartesianCoordinates(locationA);
            var (xB, yB, zB) = GetCartesianCoordinates(locationB);
            var x2 = Math.Pow(xA - xB, 2);
            var y2 = Math.Pow(yA - yB, 2);
            var z2 = Math.Pow(zA - zB, 2);
            return Math.Sqrt(x2 + y2 + z2);
        }

        private Cartesian GetCartesianCoordinates(Coordinates location)
        {
            var latitude = location.Latitude.ToRadians();
            var longitude = location.Longitude.ToRadians();
            var x = Constants.EarthRadiusKm * Math.Sin(latitude) * Math.Cos(longitude);
            var y = Constants.EarthRadiusKm * Math.Sin(latitude) * Math.Sin(longitude);
            var z = Constants.EarthRadiusKm * Math.Cos(latitude);
            return (x, y, z);
        }
    }
}
