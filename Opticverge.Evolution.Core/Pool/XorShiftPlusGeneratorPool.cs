using Opticverge.Evolution.Core.Generators;

namespace Opticverge.Evolution.Core.Pool
{
    public sealed class XorShiftPlusGeneratorPool : TypedObjectPool<XorShiftPlusGeneratorPool, XorShiftPlusGenerator>
    {
        /// <summary>
        /// Gets a generator from the pool
        /// </summary>
        /// <param name="seed">If provided will update the generator state with the seed</param>
        /// <returns></returns>
        public XorShiftPlusGenerator Get(ulong? seed)
        {
            var generator = Get();
            generator.Reset(seed);
            return generator;
        }

        /// <summary>
        /// Returns a generator to the pool
        /// </summary>
        /// <param name="obj">The generator to return</param>
        /// <param name="reset">If true will reset the generator state before being returned to the pool</param>
        public void Return(XorShiftPlusGenerator obj, bool reset = true)
        {
            if (reset)
            {
                obj.Reset();
            }

            base.Return(obj);
        }
    }
}
