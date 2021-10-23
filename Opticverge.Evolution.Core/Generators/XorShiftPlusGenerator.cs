using System;

namespace Opticverge.Evolution.Core.Generators
{
    /// <summary>
    /// A pseudo random number generator
    /// </summary>
    public class XorShiftPlusGenerator : Random
    {
        public ulong Seed { get; }

        private const double DoubleUnit = 1.0 / (int.MaxValue + 1.0);

        private ulong _x;
        private ulong _y;

        private ulong _buffer;
        private ulong _bufferMask;

        public XorShiftPlusGenerator()
        {
            Seed = SeedInitialiser.GetSeed();
            _x = Seed << 3;
            _y = Seed >> 3;
        }

        public XorShiftPlusGenerator(ulong seed)
        {
            Seed = seed == 0 ? SeedInitialiser.GetSeed() : seed;
            _x = Seed << 3;
            _y = Seed >> 3;
        }

        /// <inheritdoc />
        public override double NextDouble()
        {
            var tempX = _y;
            _x ^= _x << 23;
            var tempY = _x ^ _y ^ (_x >> 17) ^ (_y >> 26);
            var tempZ = tempY + _y;
            _x = tempX;
            _y = tempY;
            return DoubleUnit * (0x7FFFFFFF & tempZ);
        }

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to min, and less than max.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public double NextDouble(double min, double max)
        {
            return (NextDouble() * (max - min)) + min;
        }

        /// <summary>
        /// Returns a random boolean
        /// </summary>
        public bool NextBool()
        {
            if (_bufferMask > 0)
            {
                var next = (_buffer & _bufferMask) == 0;
                _bufferMask >>= 1;
                return next;
            }

            var tempX = _y;
            _x ^= _x << 23;
            var tempY = _x ^ _y ^ (_x >> 17) ^ (_y >> 26);

            _buffer = tempY + _y;
            _x = tempX;
            _y = tempY;

            _bufferMask = 0x8000000000000000;
            return (_buffer & 0xF000000000000000) == 0;
        }

        /// <summary>
        /// Returns a random boolean using <see cref="NextDouble()"/> and a bias
        /// </summary>
        /// <param name="bias">Should be within the range 0.0 and 1.0</param>
        /// <remarks>When the bias is set to 0.0 the output will always be false.
        /// When set to 1.0 the output will always be true.</remarks>
        public bool NextBool(double bias)
        {
            return NextDouble() < bias;
        }
    }
}
