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
            var target = new Problem(Objective.Maximisation);

            // assert
            Assert.Equal(Objective.Maximisation, target.Objective);
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
                Objective.Maximisation,
                concurrency: concurrency,
                populationSize: populationSize,
                tokenSource: tokenSource
            );

            // assert
            Assert.Equal(Objective.Maximisation, target.Objective);
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
            Assert.Throws<ArgumentOutOfRangeException>(() => new Problem(Objective.Maximisation, populationSize: 0));
        }

        [Fact]
        public void Concurrency_Should_BeAMinimumOfOne()
        {
            // arrange
            var target = new Problem(Objective.Maximisation, concurrency: 0);

            // act
            // assert
            Assert.True(target.Concurrency > 0);
        }

        [Fact]
        public void LifeTime_Should_SetEpochs_When_ULongSet()
        {
            // arrange
            var target = new Problem(Objective.Maximisation, lifeTime: new LifeTime(100UL));

            // act
            // assert
            Assert.NotNull(target.LifeTime);
            Assert.Equal(100UL, target.Epochs);
            Assert.Equal(0UL, target.Generation);
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
            var target = new Problem(Objective.Maximisation, lifeTime: new LifeTime(TimeSpan.FromMilliseconds(delay)));

            // act
            await Task.Delay(TimeSpan.FromMilliseconds(delay + 10));

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
            var target = new Problem(Objective.Maximisation,
                lifeTime: new LifeTime(DateTime.UtcNow.AddMilliseconds(delay)));

            // act
            await Task.Delay(TimeSpan.FromMilliseconds(delay + 10));

            // assert
            Assert.NotNull(target.LifeTime);
            Assert.True(target.TokenSource.IsCancellationRequested);
        }

        [Fact]
        public void LifeTime_Should_CancelToken_When_TerminatingConditionProvided()
        {
            // arrange
            Func<IProblem, bool> terminatingCondition = problem => true;

            var target = new Problem(Objective.Maximisation, lifeTime: new LifeTime(terminatingCondition));

            // act
            target.NextGeneration();

            // assert
            Assert.NotNull(target.LifeTime);
            Assert.True(target.TokenSource.IsCancellationRequested);
            Assert.True(target.Generation > 0);
        }

        [Fact]
        public void Generation_Should_CancelToken_When_GenerationMatchesEpochs()
        {
            // arrange
            var target = new Problem(Objective.Maximisation, lifeTime: new LifeTime(1UL));

            // act
            target.NextGeneration();

            // assert
            Assert.NotNull(target.LifeTime);
            Assert.True(target.TokenSource.IsCancellationRequested);
            Assert.Equal(target.Generation, target.Epochs);
        }

        [Fact]
        public void NextGeneration_Should_FollowProcess()
        {
            // arrange
            var target = new Problem(Objective.Maximisation);

            // act
            target.NextGeneration();

            // assert
            Assert.Null(target.LifeTime);
            Assert.False(target.TokenSource.IsCancellationRequested);
            Assert.Equal(1UL, target.Generation);
            Assert.Equal(0UL, target.Epochs);
        }

        [Fact]
        public void Initialise_Should_FollowProcess()
        {
            // arrange
            var target = new Problem(Objective.Maximisation);

            // act
            // assert
            Assert.Throws<NotImplementedException>(() => target.Initialise());
        }

        [Fact]
        public void Initialise_Should_ThrowOperationCancelledException()
        {
            // arrange
            var target = new Problem(Objective.Maximisation);

            // act
            target.TokenSource.Cancel();

            // assert
            Assert.Throws<OperationCanceledException>(() => target.Initialise());
        }

        [Fact]
        public void Create_Should_FollowProcess()
        {
            // arrange
            var target = new Problem(Objective.Maximisation);

            // act
            // assert
            Assert.Throws<NotImplementedException>(() => target.Create());
        }

        [Fact]
        public async Task Start_Should_FollowProcess()
        {
            // arrange
            var target = new Problem(Objective.Maximisation);

            // act
            target.Start();

            await Task.Delay(1);

            // assert
            Assert.NotNull(target.Summary);
            Assert.Equal(0UL, target.Summary.Generated);
            Assert.True(target.Summary.Elapsed > 0);
        }

        [Fact]
        public async Task End_Should_FollowProcess()
        {
            // arrange
            var target = new Problem(Objective.Maximisation);

            // act
            target.Start();
            await Task.Delay(1);
            target.End();

            // assert
            Assert.NotNull(target.Summary);
            Assert.Equal(0UL, target.Summary.Generated);
            Assert.True(target.Summary.Elapsed > 0);
        }
    }
}
