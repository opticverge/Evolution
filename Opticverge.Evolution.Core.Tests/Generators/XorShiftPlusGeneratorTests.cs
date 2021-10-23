using System.Linq;
using Opticverge.Evolution.Core.Generators;
using Xunit;

namespace Opticverge.Evolution.Core.Tests.Generators
{
    public class XorShiftPlusGeneratorTests
    {
        [Fact]
        public void Constructor_Should_HaveValidSeed()
        {
            // arrange
            var target = new XorShiftPlusGenerator();

            // act
            var actual = target.Seed;

            // assert
            Assert.NotEqual(0UL, actual);
        }

        [Fact]
        public void Constructor_Should_HaveValidSeed_WhenSeedProvided()
        {
            // arrange
            var target = new XorShiftPlusGenerator(0UL);

            // act
            var actual = target.Seed;

            // assert
            Assert.NotEqual(0UL, actual);
        }

        [Fact]
        public void NextDouble_Should_FollowProcess()
        {
            // arrange
            var target = new XorShiftPlusGenerator();

            // act
            var actual = target.NextDouble();

            // assert
            Assert.InRange(actual, 0.0, 1.0);
        }

        [Fact]
        public void NextDouble_Should_FollowProcess_When_SeedIsOne()
        {
            // arrange
            var target = new XorShiftPlusGenerator(1UL);

            // act
            var actual = target.NextDouble();

            // assert
            Assert.InRange(actual, 0.0, 1.0);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(100000)]
        public void NextDouble_Should_FollowProcess_When_CalledManyTimes(
            int quantity
        )
        {
            // arrange
            var target = new XorShiftPlusGenerator(1UL);

            var generated = new double[quantity];

            for (int i = 0; i < quantity; i++)
            {
                generated[i] = target.NextDouble();
            }

            // act
            var actual = generated.Average();

            // assert
            Assert.InRange(actual, 0.0, 1.0);
        }

        [Theory]
        [InlineData(1, 10)]
        [InlineData(ulong.MinValue, 10)]
        [InlineData(ulong.MaxValue, 10)]
        [InlineData(100, 10)]
        public void NextDouble_Should_FollowProcess_When_SeedProvided(
            ulong seed,
            int quantity
        )
        {
            // arrange
            var target = new XorShiftPlusGenerator(seed);

            var generated = new double[quantity];

            for (int i = 0; i < quantity; i++)
            {
                generated[i] = target.NextDouble();
            }

            // act
            var actual = generated.Average();

            // assert#
            Assert.True(actual > 0.0);
            Assert.InRange(actual, 0.0, 1.0);
        }

        [Theory]
        [InlineData(0.0, 0.5)]
        [InlineData(-0.5, 0.0)]
        [InlineData(-0.5, -0.1)]
        [InlineData(-100.0, -50.0)]
        [InlineData(50.0, 100.0)]
        [InlineData(0.0, double.MaxValue)]
        [InlineData(double.MinValue, 0.0)]
        public void NextDouble_Should_FollowProcess_When_MinMaxProvided(
            double min,
            double max
        )
        {
            // arrange
            var target = new XorShiftPlusGenerator();

            // act
            var actual = target.NextDouble(min, max);

            // assert
            Assert.InRange(actual, min, max);
        }

        [Fact]
        public void NextBool_Should_FollowProcess()
        {
            // arrange
            var target = new XorShiftPlusGenerator();

            // act
            var actual = target.NextBool();

            // assert
            Assert.IsType<bool>(actual);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(100000)]
        public void NextBool_Should_FollowProcess_When_CalledManyTimes(
            int quantity
        )
        {
            // arrange
            var target = new XorShiftPlusGenerator();

            // act
            var generated = new bool[quantity];

            for (int i = 0; i < quantity; i++)
            {
                generated[i] = target.NextBool();
            }

            // act
            var actualTrue = generated.Count(v => v is true);
            var actualFalse = generated.Count(v => v is false);

            // assert
            Assert.True(actualTrue > 0);
            Assert.True(actualFalse > 0);
        }

        [Theory]
        [InlineData(0.0, false)]
        [InlineData(1.0, true)]
        public void NextBool_WithBias_Should_FollowProcess(
            double bias,
            bool expected
        )
        {
            // arrange
            var target = new XorShiftPlusGenerator();

            // act
            var actual = target.NextBool(bias);

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
