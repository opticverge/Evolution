using System;
using System.Threading;
using AutoFixture.Xunit2;
using Moq;
using Opticverge.Evolution.Core.Algorithm;
using Opticverge.Evolution.Core.Problems;
using Xunit;

namespace Opticverge.Evolution.Core.Tests.Algorithm
{
    public class EvolutionaryAlgorithmTests
    {
        [Fact]
        public void Constructor_Should_FollowProcess()
        {
            // arrange
            var target = new EvolutionaryAlgorithm();

            // act
            // assert
            Assert.NotNull(target);
        }

        [Theory]
        [AutoData]
        public void Run_Should_FollowProcess(
            Mock<IProblem> problemMock,
            CancellationTokenSource cancellationTokenSource
        )
        {
            // arrange
            problemMock
                .Setup(m => m.TokenSource)
                .Returns(cancellationTokenSource);

            var target = new EvolutionaryAlgorithm();

            // act
            Assert.Throws<NotImplementedException>(() => target.Run(problemMock.Object));

            // assert
            problemMock
                .Verify(
                    m => m.Start(),
                    Times.Once
                );

            problemMock
                .Verify(
                    m => m.Initialise(),
                    Times.Once
                );

            problemMock
                .Verify(
                    m => m.Evaluate(),
                    Times.Once
                );
            problemMock
                .Verify(
                    m => m.Sort(),
                    Times.Once
                );

            problemMock
                .Verify(
                    m => m.NextGeneration(),
                    Times.Once
                );

            problemMock
                .Verify(
                    m => m.End(),
                    Times.Once
                );
        }
    }
}
