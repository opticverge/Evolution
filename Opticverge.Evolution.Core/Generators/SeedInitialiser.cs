using System;
using System.Threading;

namespace Opticverge.Evolution.Core.Generators
{
    public static class SeedInitialiser
    {
        private static ulong _currentSeed = (ulong)Guid.NewGuid().GetHashCode();

        public static ulong Seed => _currentSeed;

        public static ulong GetSeed()
        {
            return Interlocked.Add(ref _currentSeed, 1UL);
        }
    }
}
