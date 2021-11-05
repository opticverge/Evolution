using Opticverge.Evolution.Core.Pool;
using Xunit;

namespace Opticverge.Evolution.Core.Tests.Pool
{
    public class XorShiftPlusGeneratorPoolTests
    {
        [Fact]
        public void Constructor_Should_FollowProcess()
        {
            // arrange
            var expected = XorShiftPlusGeneratorPool.Instance;

            // act
            var actual = XorShiftPlusGeneratorPool.Instance;

            // assert
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Get_Should_FollowProcess()
        {
            // arrange
            var target = XorShiftPlusGeneratorPool.Instance;

            var expected = target.Get();

            // act
            var actual = target.Get();

            // assert
            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void Get_Should_FollowProcess_When_SeedsProvided()
        {
            // arrange
            var expected1 = 1UL;
            var expected2 = 2UL;

            var target = XorShiftPlusGeneratorPool.Instance;

            var generator1 = target.Get(expected1);
            var generator2 = target.Get(expected2);

            // act
            var actual1 = generator1.Seed;
            var actual2 = generator2.Seed;

            // assert
            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
        }

        [Fact]
        public void Return_Should_FollowProcess()
        {
            // arrange
            var target = XorShiftPlusGeneratorPool.Instance;
            var expected = target.Get();
            target.Return(expected);

            // act
            var actual = target.Get();

            // assert
            Assert.Equal(expected, actual);
            Assert.Equal(expected.Seed, actual.Seed);
        }

        [Fact]
        public void Return_Should_FollowProcess_When_SeedProvided()
        {
            // arrange
            var target = XorShiftPlusGeneratorPool.Instance;
            var expected = target.Get(1);
            target.Return(expected);

            // act
            var actual = target.Get(1);

            // assert
            Assert.Equal(expected, actual);
            Assert.Equal(expected.Seed, actual.Seed);
        }

        [Fact]
        public void Return_Should_FollowProcess_When_ResetIsTrue()
        {
            // arrange
            var target = XorShiftPlusGeneratorPool.Instance;
            var expected = target.Get();
            target.Return(expected, true);

            // act
            var actual = target.Get();

            // assert
            Assert.Equal(expected, actual);
            Assert.Equal(expected.Seed, actual.Seed);
        }

        [Fact]
        public void Return_Should_FollowProcess_When_ResetIsFalse()
        {
            // arrange
            var target = XorShiftPlusGeneratorPool.Instance;
            var expected = target.Get();
            target.Return(expected, false);

            // act
            var actual = target.Get();

            // assert
            Assert.Equal(expected, actual);
            Assert.Equal(expected.Seed, actual.Seed);
        }
    }
}
