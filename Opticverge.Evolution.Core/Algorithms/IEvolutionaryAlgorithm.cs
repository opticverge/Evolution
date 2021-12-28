using Opticverge.Evolution.Core.Problems;

namespace Opticverge.Evolution.Core.Algorithms
{
    public interface IEvolutionaryAlgorithm
    {
        void Evolve(IProblem problem);
        void Run(IProblem problem);
    }
}
