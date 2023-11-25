using FluentValidation.TestHelper;
using RoomexEarth.Api.Models;
using RoomexEarth.Api.Validators;
using RoomexEarth.Logic.Models;
using RoomexEarth.Tests.Common;

namespace RoomexEarth.Api.Tests.Validators
{
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.Self)]
    [TestFixture(Category = "Validators")]
    internal class CalculateDistanceRequestValidatorTests
    {
        private readonly CustomFixture _fixture;
        private readonly CalculateDistanceRequestValidator _sut;

        public CalculateDistanceRequestValidatorTests()
        {
            _fixture = new();
            _sut = new();
        }

        [Test]
        public void CalculateDistanceRequestValidator_too_negative_LocationA_Latitude()
        {
            var instance = _fixture.Build<CalculateDistanceRequest>()
                                   .With(_ => _.LocationA, new Coordinates(-100, 0))
                                   .Create();

            var validationResult = _sut.TestValidate(instance).Errors;

            Assert.That(validationResult.Count(), Is.EqualTo(1), "Expected one error");
            Assert.That(validationResult[0].ErrorMessage, Is.EqualTo("'Location A Latitude' must be greater than or equal to '-90'."), "Unexpected message");
        }

        [Test]
        public void CalculateDistanceRequestValidator_too_positive_LocationA_Latitude()
        {
            var instance = _fixture.Build<CalculateDistanceRequest>()
                                   .With(_ => _.LocationA, new Coordinates(100, 0))
                                   .Create();

            var validationResult = _sut.TestValidate(instance).Errors;

            Assert.That(validationResult.Count(), Is.EqualTo(1), "Expected one error");
            Assert.That(validationResult[0].ErrorMessage, Is.EqualTo("'Location A Latitude' must be less than or equal to '90'."), "Unexpected message");
        }

        [Test]
        public void CalculateDistanceRequestValidator_too_negative_LocationA_Longitude()
        {
            var instance = _fixture.Build<CalculateDistanceRequest>()
                                   .With(_ => _.LocationA, new Coordinates(0, -200))
                                   .Create();

            var validationResult = _sut.TestValidate(instance).Errors;

            Assert.That(validationResult.Count(), Is.EqualTo(1), "Expected one error");
            Assert.That(validationResult[0].ErrorMessage, Is.EqualTo("'Location A Longitude' must be greater than or equal to '-180'."), "Unexpected message");
        }

        [Test]
        public void CalculateDistanceRequestValidator_too_positive_LocationA_Longitude()
        {
            var instance = _fixture.Build<CalculateDistanceRequest>()
                                   .With(_ => _.LocationA, new Coordinates(0, 200))
                                   .Create();

            var validationResult = _sut.TestValidate(instance).Errors;

            Assert.That(validationResult.Count(), Is.EqualTo(1), "Expected one error");
            Assert.That(validationResult[0].ErrorMessage, Is.EqualTo("'Location A Longitude' must be less than or equal to '180'."), "Unexpected message");
        }

        [Test]
        public void CalculateDistanceRequestValidator_too_negative_LocationB_Latitude()
        {
            var instance = _fixture.Build<CalculateDistanceRequest>()
                                   .With(_ => _.LocationB, new Coordinates(-100, 0))
                                   .Create();

            var validationResult = _sut.TestValidate(instance).Errors;

            Assert.That(validationResult.Count(), Is.EqualTo(1), "Expected one error");
            Assert.That(validationResult[0].ErrorMessage, Is.EqualTo("'Location B Latitude' must be greater than or equal to '-90'."), "Unexpected message");
        }

        [Test]
        public void CalculateDistanceRequestValidator_too_positive_LocationB_Latitude()
        {
            var instance = _fixture.Build<CalculateDistanceRequest>()
                                   .With(_ => _.LocationB, new Coordinates(100, 0))
                                   .Create();

            var validationResult = _sut.TestValidate(instance).Errors;

            Assert.That(validationResult.Count(), Is.EqualTo(1), "Expected one error");
            Assert.That(validationResult[0].ErrorMessage, Is.EqualTo("'Location B Latitude' must be less than or equal to '90'."), "Unexpected message");
        }

        [Test]
        public void CalculateDistanceRequestValidator_too_negative_LocationB_Longitude()
        {
            var instance = _fixture.Build<CalculateDistanceRequest>()
                                   .With(_ => _.LocationB, new Coordinates(0, -200))
                                   .Create();

            var validationResult = _sut.TestValidate(instance).Errors;

            Assert.That(validationResult.Count(), Is.EqualTo(1), "Expected one error");
            Assert.That(validationResult[0].ErrorMessage, Is.EqualTo("'Location B Longitude' must be greater than or equal to '-180'."), "Unexpected message");
        }

        [Test]
        public void CalculateDistanceRequestValidator_too_positive_LocationB_Longitude()
        {
            var instance = _fixture.Build<CalculateDistanceRequest>()
                                   .With(_ => _.LocationB, new Coordinates(0, 200))
                                   .Create();

            var validationResult = _sut.TestValidate(instance).Errors;

            Assert.That(validationResult.Count(), Is.EqualTo(1), "Expected one error");
            Assert.That(validationResult[0].ErrorMessage, Is.EqualTo("'Location B Longitude' must be less than or equal to '180'."), "Unexpected message");
        }

        [Test]
        public void CalculateDistanceRequestValidator_multiple_errors()
        {
            var instance = _fixture.Build<CalculateDistanceRequest>()
                                   .With(_ => _.LocationA, new Coordinates(-100, -200))
                                   .With(_ => _.LocationB, new Coordinates(-100, -200))
                                   .Create();

            var validationResult = _sut.TestValidate(instance).Errors;

            Assert.That(validationResult.Count(), Is.EqualTo(4), "Expected one error");
            Assert.That(validationResult[0].ErrorMessage, Is.EqualTo("'Location A Latitude' must be greater than or equal to '-90'."), "Unexpected message");
            Assert.That(validationResult[1].ErrorMessage, Is.EqualTo("'Location A Longitude' must be greater than or equal to '-180'."), "Unexpected message");
            Assert.That(validationResult[2].ErrorMessage, Is.EqualTo("'Location B Latitude' must be greater than or equal to '-90'."), "Unexpected message");
            Assert.That(validationResult[3].ErrorMessage, Is.EqualTo("'Location B Longitude' must be greater than or equal to '-180'."), "Unexpected message");
        }
    }
}
