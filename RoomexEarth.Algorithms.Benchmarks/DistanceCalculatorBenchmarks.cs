using BenchmarkDotNet.Attributes;
using RoomexEarth.Algorithms.DistanceCalculator;
using RoomexEarth.Logic.Algorithms;
using RoomexEarth.Logic.Models;

namespace RoomexEarth.Algorithms.Benchmarks
{
    public class DistanceCalculatorBenchmarks<T> where T : IDistanceCalculator, new()
    {
        private static readonly Coordinates _locationA = new(-33, -56);
        private static readonly Coordinates _locationB = new(40, 12);

        private readonly T _sut;

        protected DistanceCalculatorBenchmarks() => _sut = new();

        [Benchmark]
        public void DistanceSameLocation() => _sut.CalculateDistance(_locationA, _locationA);

        [Benchmark]
        public void DistanceDifferentLocations() => _sut.CalculateDistance(_locationA, _locationB);
    }

    public class Cosine : DistanceCalculatorBenchmarks<CosineDistanceCalculator> { }

    public class Pythagoras : DistanceCalculatorBenchmarks<PythagorasDistanceCalculator> { }
}
