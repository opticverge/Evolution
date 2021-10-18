using System;

namespace Opticverge.Evolution.Core.Generators
{
    public class XorShiftPlusGenerator : Random
    {
        public ulong Seed { get; }

        private const double DoubleUnit = 1.0 / (int.MaxValue + 1.0);

        private ulong _x;
        private ulong _y;

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
    }
}
