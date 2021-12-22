using Opticverge.Evolution.Core.LifeCycle;

namespace Opticverge.Evolution.Core.Chromosomes
{
    public interface IChromosome : ILifeCycle
    {
        void Generate();
        void Mutate();
    }
}
