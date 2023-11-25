namespace RoomexEarth.Algorithms
{
    /// <summary>
    /// Provides extension methods used by the algorithms.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Convert degrees to radians.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>The angle in radians.</returns>
        internal static double ToRadians(this double degrees) => degrees * Math.PI / 180;
    }
}
