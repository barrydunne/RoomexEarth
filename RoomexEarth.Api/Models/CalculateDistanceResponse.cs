namespace RoomexEarth.Api.Models
{
    /// <summary>
    /// The response to a <see cref="CalculateDistanceRequest"/> request.
    /// </summary>
    public class CalculateDistanceResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalculateDistanceResponse"/> class.
        /// </summary>
        /// <param name="distanceKm">The distance in kilometres.</param>
        public CalculateDistanceResponse(double distanceKm) => DistanceKm = distanceKm;

        /// <summary>
        /// Gets the distance in kilometres.
        /// </summary>
        public double DistanceKm { get; init; }

        /// <summary>
        /// Gets the distance in miles.
        /// </summary>
        public double DistanceMiles => DistanceKm * 0.62137119;
    }
}
