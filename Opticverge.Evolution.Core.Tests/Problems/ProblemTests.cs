using System;
using System.Threading;
using System.Threading.Tasks;
using Opticverge.Evolution.Core.Problems;
using Xunit;

namespace Opticverge.Evolution.Core.Tests.Problems
{
    public class ProblemTests
    {
        [Fact]
        public void Constructor_Should_FollowProcess()
        {
            // arrange
            // act
            var target = new Problem();

            // assert
            Assert.NotNull(target.TokenSource);
            Assert.False(target.TokenSource.IsCancellationRequested);
            Assert.True(target.Concurrency > 0);
            Assert.NotNull(target.Generated);
            Assert.True(target.Generation == 0);
            Assert.NotNull(target.Population);
            Assert.True(target.PopulationSize > 0);
            Assert.Null(target.LifeTime);
            Assert.Equal(0UL, target.Epochs);
        }

        [Theory]
        [InlineData(1, 10)]
        [InlineData(2, 20)]
        public void Constructor_Should_FollowProcess_When_DifferentArgumentsProvided(
            int concurrency,
            int populationSize
        )
        {
            // arrange
            var tokenSource = new CancellationTokenSource();

            // act
            var target = new Problem(
                concurrency: concurrency,
                populationSize: populationSize,
                tokenSource: tokenSource
            );

            // assert
            Assert.NotNull(target.TokenSource);
            Assert.False(target.TokenSource.IsCancellationRequested);
            Assert.Same(tokenSource, target.TokenSource);
            Assert.True(target.Concurrency > 0);
            Assert.NotNull(target.Generated);
            Assert.Equal(0UL, target.Generation);
            Assert.NotNull(target.Population);
            Assert.True(target.PopulationSize > 0);
            Assert.Null(target.LifeTime);
            Assert.Equal(0UL, target.Epochs);
        }

        [Fact]
        public void Constructor_Should_ThrowException_When_PopulationSizeIsZero()
        {
            // arrange
            // act
            // assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Problem(populationSize: 0));
        }

        [Fact]
        public void Concurrency_Should_BeAMinimumOfOne()
        {
            // arrange
            var target = new Problem(concurrency: 0);

            // act
            // assert
            Assert.True(target.Concurrency > 0);
        }

        [Fact]
        public void LifeTime_Should_SetEpochs_When_ULongSet()
        {
            // arrange
            var target = new Problem(lifeTime: new LifeTime(100UL));

            // act
            // assert
            Assert.NotNull(target.LifeTime);
            Assert.Equal(100UL, target.Epochs);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public async Task LifeTime_Should_CancelToken_When_TimeSpanSet(
            int delay
        )
        {
            // arrange
            var target = new Problem(lifeTime: new LifeTime(TimeSpan.FromMilliseconds(delay)));

            // act
            await Task.Delay(TimeSpan.FromMilliseconds(delay));

            // assert
            Assert.NotNull(target.LifeTime);
            Assert.True(target.TokenSource.IsCancellationRequested);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public async Task LifeTime_Should_CancelToken_When_DateTimeSet(int delay)
        {
            // arrange
            var dateTime = DateTime.UtcNow.AddMilliseconds(1);
            var target = new Problem(lifeTime: new LifeTime(dateTime));

            // act
            await Task.Delay(TimeSpan.FromMilliseconds(delay));

            // assert
            Assert.NotNull(target.LifeTime);
            Assert.True(target.TokenSource.IsCancellationRequested);
        }

        [Fact]
        public void LifeTime_Should_CancelToken_When_TerminatingConditionProvided()
        {
            // arrange
            Func<IProblem, bool> terminatingCondition = problem =>
            {
                problem.TokenSource.Cancel();
                return true;
            };

            var target = new Problem(lifeTime: new LifeTime(terminatingCondition));

            // act
            // assert
            Assert.NotNull(target.LifeTime);
            Assert.False(target.TokenSource.IsCancellationRequested);
        }

        [Fact]
        public void Initialise_Should_FollowProcess()
        {
            // arrange
            var target = new Problem();

            // act
            // assert
            Assert.Throws<NotImplementedException>(() => target.Initialise());
        }

        [Fact]
        public void Initialise_Should_ThrowOperationCancelledException()
        {
            // arrange
            var target = new Problem();

            // act
            target.TokenSource.Cancel();

            // assert
            Assert.Throws<OperationCanceledException>(() => target.Initialise());
        }
    }
}
