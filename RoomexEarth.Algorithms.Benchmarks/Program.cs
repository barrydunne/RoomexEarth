using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using RoomexEarth.Algorithms.Benchmarks;

BenchmarkRunner.Run([typeof(Cosine), typeof(Pythagoras)], DefaultConfig.Instance.WithOption(ConfigOptions.JoinSummary, true));
