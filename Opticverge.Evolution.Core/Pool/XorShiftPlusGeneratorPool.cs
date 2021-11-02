using Opticverge.Evolution.Core.Generators;

namespace Opticverge.Evolution.Core.Pool
{
    public sealed class XorShiftPlusGeneratorPool : GenericObjectPool<XorShiftPlusGeneratorPool, XorShiftPlusGenerator>
    {
        public XorShiftPlusGenerator Get(ulong? seed)
        {
            return new XorShiftPlusGenerator(seed);
        }
    }
}
