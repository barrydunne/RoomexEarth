using AutoFixture;

namespace RoomexEarth.Tests.Common
{
    /// <summary>
    /// Provides customizations for AutoFixture data.
    /// </summary>
    public class CustomFixture : Fixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFixture"/> class.
        /// </summary>
        public CustomFixture() => Customizations.Add(new CustomSpecimenBuilder());
    }
}
