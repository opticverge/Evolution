using Opticverge.Evolution.Core.Generators;
using Opticverge.Evolution.Core.Pool;
using Xunit;

namespace Opticverge.Evolution.Core.Tests.Pool
{
    public class GenericObjectPoolTests
    {
        [Fact]
        public void Instance_Should_FollowProcess()
        {
            // arrange
            var actual = GenericObjectPool<object>.Instance;

            // act
            // assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void Instance_Should_BeTheSame()
        {
            // arrange
            var expected = GenericObjectPool<object>.Instance;

            // act
            var actual = GenericObjectPool<object>.Instance;

            // assert
            Assert.Same(expected, actual);
        }

        [Fact]
        public void DifferentTypes_Should_NotBeTheSame()
        {
            // arrange
            var expected = GenericObjectPool<object>.Instance;

            // act
            var actual = GenericObjectPool<XorShiftPlusGenerator>.Instance;

            // assert
            Assert.NotSame(expected, actual);
        }

        [Fact]
        public void Get_Should_NotBeTheSame()
        {
            // arrange
            var expected = GenericObjectPool<object>.Instance.Get();

            // act
            var actual = GenericObjectPool<object>.Instance.Get();

            // assert
            Assert.NotSame(expected, actual);
        }

        [Fact]
        public void GetReturn_Should_BeTheSame()
        {
            // arrange
            var expected = GenericObjectPool<object>.Instance.Get();
            GenericObjectPool<object>.Instance.Return(expected);

            // act
            var actual = GenericObjectPool<object>.Instance.Get();

            // assert
            Assert.Same(expected, actual);
        }
    }
}
