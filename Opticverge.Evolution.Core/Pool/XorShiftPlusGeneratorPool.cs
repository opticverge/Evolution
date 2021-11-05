using Opticverge.Evolution.Core.Generators;

namespace Opticverge.Evolution.Core.Pool
{
    public sealed class XorShiftPlusGeneratorPool : TypedObjectPool<XorShiftPlusGeneratorPool, XorShiftPlusGenerator>
    {
        public XorShiftPlusGenerator Get(ulong? seed)
        {
            var generator = Get();
            generator.Reset(seed);
            return generator;
        }
    }
}
