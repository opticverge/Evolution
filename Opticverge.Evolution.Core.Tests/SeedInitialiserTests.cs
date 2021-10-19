using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Opticverge.Evolution.Core.Generators;
using Xunit;

namespace Opticverge.Evolution.Core.Tests
{
    public class SeedInitialiserTests
    {
        [Fact]
        public void Seed_Should_NotReturnZero()
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
        [InlineData(100, 1)]
        [InlineData(1000, 1)]
        [InlineData(10000, 1)]
        [InlineData(100000, 1)]
        [InlineData(1000000, 1)]
        [InlineData(100, 2)]
        [InlineData(1000, 2)]
        [InlineData(10000, 2)]
        [InlineData(100000, 2)]
        [InlineData(1000000, 2)]
        [InlineData(100, 4)]
        [InlineData(1000, 4)]
        [InlineData(10000, 4)]
        [InlineData(100000, 4)]
        [InlineData(1000000, 4)]
        public async Task SeedInitialiser_Should_BeThreadSafe(
            ulong quantity,
            int maxDegreeOfParallelism
        )
        {
            // arrange
            var bag = new ConcurrentBag<ulong>();

            var actionBlock = new ActionBlock<ulong>(
                _ => bag.Add(SeedInitialiser.GetSeed()),
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = maxDegreeOfParallelism
                }
            );

            // act
            for (ulong i = 0; i < quantity; i++)
            {
                await actionBlock.SendAsync(i);
            }

            actionBlock.Complete();

            await actionBlock.Completion;

            // assert
            Assert.Equal(quantity, (ulong)bag.Distinct().Count());
        }
    }
}
