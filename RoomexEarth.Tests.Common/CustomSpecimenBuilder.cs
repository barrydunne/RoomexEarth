using AutoFixture.Kernel;
using RoomexEarth.Logic.Models;
using System.Reflection;

namespace RoomexEarth.Tests.Common
{
    /// <summary>
    /// Used to generate random <see cref="Coordinates"/> with valid latitude and longitude.
    /// </summary>
    internal class CustomSpecimenBuilder : ISpecimenBuilder
    {
        private static readonly Type _typeCoordinates = typeof(Coordinates);

        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type)
            {
                if (type == _typeCoordinates)
                    return CreateCoordinates();
            }
            else if (request is PropertyInfo prop)
            {
                if (prop.PropertyType == _typeCoordinates)
                    return CreateCoordinates();
            }
            return new NoSpecimen();
        }

        private Coordinates CreateCoordinates() => new(CreateLatitude(), CreateLongitude());
        private double CreateLatitude() => CreateDouble(-90, 90);
        private double CreateLongitude() => CreateDouble(-180, 180);
        private double CreateDouble(int min, int max) => min + (Random.Shared.NextDouble() * (max - min));
    }
}
