namespace Opticverge.Evolution.Core.Chromosomes
{
    public interface IChromosome
    {
        void Generate();
        void Mutate();
    }
}