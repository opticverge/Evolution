using System;
using Opticverge.Evolution.Core.Chromosomes;
using Xunit;

namespace Opticverge.Evolution.Core.Tests.Chromosomes
{
    public class ChromosomeTests
    {
        [Fact]
        public void Constructor_Should_FollowProcess()
        {
            // arrange
            // act
            var target = new Chromosome<double>();

            // assert
            Assert.Equal(default(double), target.Phenotype);
            Assert.Null(target.Seed);
            Assert.NotNull(target.Generator);
            Assert.NotNull(target.IdBuilder);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(0UL)]
        [InlineData(1UL)]
        [InlineData(ulong.MaxValue)]
        public void Constructor_Should_Initialize_When_SeedIsProvided(
            ulong? seed
        )
        {
            // arrange
            // act
            var target = new Chromosome<double>(seed);

            // assert
            Assert.Equal(default(double), target.Phenotype);
            Assert.Equal(seed, target.Seed);
            Assert.NotNull(target.Generator);
            Assert.NotNull(target.IdBuilder);
        }

        [Fact]
        public void Generate_Should_ThrowNotImplementedException()
        {
            // arrange
            var target = new Chromosome<double>();

            // act
            // assert
            Assert.Throws<NotImplementedException>(() => target.Generate());
        }

        [Fact]
        public void Mutate_Should_ThrowNotImplementedException()
        {
            // arrange
            var target = new Chromosome<double>();

            // act
            // assert
            Assert.Throws<NotImplementedException>(() => target.Mutate());
        }

        [Fact]
        public void Setup_Should_FollowProcess()
        {
            // arrange
            // act
            var target = new Chromosome<double>();

            // assert
            Assert.Null(target.Seed);
            Assert.NotNull(target.Generator);
            Assert.NotNull(target.IdBuilder);
        }

        [Fact]
        public void Setup_Should_Reset_When_CalledMultipleTimes()
        {
            // arrange
            var target = new Chromosome<double>();

            var previousSeed = target.Generator.Seed;
            var expectedGenerator = target.Generator;
            var expectedIdBuilder = target.IdBuilder;

            // act
            target.Setup();

            // assert
            Assert.NotEqual(previousSeed, target.Generator.Seed);
            Assert.Same(expectedGenerator, target.Generator);
            Assert.Same(expectedIdBuilder, target.IdBuilder);
            Assert.Equal(default(double), target.Phenotype);
        }

        [Fact]
        public void Generate_Should_FollowProcess_When_DefaultValueProvided()
        {
            // arrange
            var target = new Chromosome<double>();

            // act
            var actual = target.Generate(null, 1.0);

            // assert
            Assert.Equal(1.0, actual);
        }

        [Fact]
        public void Generate_Should_FollowProcess_When_ValueProvided()
        {
            // arrange
            var target = new Chromosome<double>();

            // act
            var actual = target.Generate(0.5, 1.0);

            // assert
            Assert.Equal(0.5, actual);
        }

        [Fact]
        public void Generate_Should_FollowProcess_When_ChromosomeProvided()
        {
            // arrange
            var target = new Chromosome<double>();

            var chromosome = new Chromosome<double>();

            // act
            // assert
            Assert.Throws<NotImplementedException>(() => target.Generate(chromosome, 1.0));
        }

        [Fact]
        public void Mutate_Should_FollowProcess_When_DefaultValueProvided()
        {
            // arrange
            var target = new Chromosome<double>();

            // act
            var actual = target.Mutate(null, 1.0);

            // assert
            Assert.Equal(1.0, actual);
        }

        [Fact]
        public void Mutate_Should_FollowProcess_When_ValueProvided()
        {
            // arrange
            var target = new Chromosome<double>();

            // act
            var actual = target.Mutate(0.5, 1.0);

            // assert
            Assert.Equal(0.5, actual);
        }

        [Fact]
        public void Mutate_Should_FollowProcess_When_ChromosomeProvided()
        {
            // arrange
            var target = new Chromosome<double>();

            var chromosome = new Chromosome<double>();

            // act
            // assert
            Assert.Throws<NotImplementedException>(() => target.Mutate(chromosome, 1.0));
        }
    }
}
