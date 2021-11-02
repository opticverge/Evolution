using Opticverge.Evolution.Core.Pool;
using Xunit;

namespace Opticverge.Evolution.Core.Tests.Pool
{
    public class GenericArrayPoolTests
    {
        [Fact]
        public void Instance_Should_FollowProcess()
        {
            // arrange
            var actual = GenericArrayPool<int>.Instance;

            // act
            // assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void TypedPool_Should_Not_BeTheSame()
        {
            // arrange
            var expected = GenericArrayPool<int>.Instance;

            // act
            var actual = GenericArrayPool<double>.Instance;

            // assert
            Assert.NotSame(expected, actual);
        }

        [Fact]
        public void Instance_Should_BeTheSame()
        {
            // arrange
            var expected = GenericArrayPool<int>.Instance;

            // act
            var actual = GenericArrayPool<int>.Instance;

            // assert
            Assert.Same(expected, actual);
        }

        [Fact]
        public void Rent_Should_FollowProcess()
        {
            // arrange
            var target = GenericArrayPool<int>.Instance;

            var expected = target.Rent(3);

            // act
            var actual = target.Rent(3);

            // assert
            Assert.NotSame(expected, actual);
        }

        [Fact]
        public void Rent_Return_Should_FollowProcess()
        {
            // arrange
            var target = GenericArrayPool<int>.Instance;

            var expected = target.Rent(3);

            target.Return(expected);

            // act
            var actual = target.Rent(3);

            // assert
            Assert.Same(expected, actual);
        }
    }
}
