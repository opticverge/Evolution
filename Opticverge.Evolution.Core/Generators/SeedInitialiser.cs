using System;
using System.Threading;

namespace Opticverge.Evolution.Core.Generators
{
    /// <summary>
    /// Provides a threadsafe implementation for retrieveing a unique
    /// seed that is incremented when a new seed is requested.
    /// </summary>
    public static class SeedInitialiser
    {
        private static ulong _currentSeed = (ulong)Guid.NewGuid().GetHashCode();

        /// <summary>
        /// Returns the current seed.
        /// </summary>
        public static ulong Seed => _currentSeed;

        /// <summary>
        /// Returns the current seed incremented by one.
        /// </summary>
        /// <returns></returns>
        public static ulong GetSeed()
        {
            return Interlocked.Add(ref _currentSeed, 1UL);
        }
    }
}
