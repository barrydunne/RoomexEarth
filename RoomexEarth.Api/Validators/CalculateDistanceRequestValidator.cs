using FluentValidation;
using RoomexEarth.Api.Models;

namespace RoomexEarth.Api.Validators
{
    /// <summary>
    /// Validation rules for <see cref="CalculateDistanceRequest"/>.
    /// </summary>
    public class CalculateDistanceRequestValidator : AbstractValidator<CalculateDistanceRequest>
    {
        private const double _minLatitude = -90;
        private const double _maxLatitude = 90;
        private const double _minLongitude = -180;
        private const double _maxLongitude = 180;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculateDistanceRequestValidator"/> class.
        /// </summary>
        public CalculateDistanceRequestValidator()
        {
            RuleFor(_ => _.LocationA.Latitude)
                .GreaterThanOrEqualTo(_minLatitude)
                .LessThanOrEqualTo(_maxLatitude);

            RuleFor(_ => _.LocationA.Longitude)
                .GreaterThanOrEqualTo(_minLongitude)
                .LessThanOrEqualTo(_maxLongitude);

            RuleFor(_ => _.LocationB.Latitude)
                .GreaterThanOrEqualTo(_minLatitude)
                .LessThanOrEqualTo(_maxLatitude);

            RuleFor(_ => _.LocationB.Longitude)
                .GreaterThanOrEqualTo(_minLongitude)
                .LessThanOrEqualTo(_maxLongitude);
        }
    }
}
