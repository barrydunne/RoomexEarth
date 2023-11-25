namespace RoomexEarth.Logic.Models
{
    /// <summary>
    /// A geographic coordinate using Latitude and Longitude format.
    /// </summary>
    /// <param name="Latitude">The latitude.</param>
    /// <param name="Longitude">The longitude.</param>
    public record Coordinates(double Latitude, double Longitude);
}
