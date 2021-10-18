using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Opticverge.Evolution.Core.Generators;
using Xunit;

namespace Opticverge.Evolution.Core.Tests
{
    public class SeedInitialiserTests
    {
        [Fact]
        public void Seed_Should_Not_Return_Zero()
        {
            // arrange
            // act
            var actual = SeedInitialiser.Seed;

            // assert
            Assert.NotEqual(0UL, actual);
        }

        [Fact]
        public void GetSeed_Should_IncrementSeed()
        {
            // arrange
            var currentSeed = SeedInitialiser.Seed;

            // act
            var actual = SeedInitialiser.GetSeed();

            // assert
            Assert.NotEqual(currentSeed, actual);
        }

        [Theory]
        // [InlineData(1)]
        // [InlineData(10)]
        // [InlineData(100)]
        [InlineData(1000, 1)]
        [InlineData(1000, 2)]
        [InlineData(1000, 4)]
        // [InlineData(10000)]
        // [InlineData(100000)]
        // [InlineData(1000000)]
        // [InlineData(10000000)]
        public async Task SeedInitialiser_Should_BeThreadSafe(
            ulong quantity,
            int maxDegreeOfParallelism
        )
        {
            // arrange
            var initialSeed = SeedInitialiser.GetSeed();

            var actionBlock = new ActionBlock<ulong>(
                _ => SeedInitialiser.GetSeed(),
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = maxDegreeOfParallelism
                }
            );

            // act

            for (ulong i = 0; i < quantity; i++)
            {
                actionBlock.Post(i);
            }

            actionBlock.Complete();

            await actionBlock.Completion;

            // assert
            Assert.Equal(initialSeed + quantity, SeedInitialiser.Seed);
        }
    }
}
