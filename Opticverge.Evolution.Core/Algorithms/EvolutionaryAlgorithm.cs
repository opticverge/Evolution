using System;
using Opticverge.Evolution.Core.Problems;

namespace Opticverge.Evolution.Core.Algorithms
{
    public class EvolutionaryAlgorithm : IEvolutionaryAlgorithm
    {
        public void Run(IProblem problem)
        {
            try
            {
                problem.Start();

                problem.Initialise();

                problem.Evaluate();

                problem.Sort();

                while (!problem.TokenSource.IsCancellationRequested)
                {
                    problem.NextGeneration();
                    Evolve(problem);
                }
            }
            finally
            {
                problem.End();
            }
        }

        public virtual void Evolve(IProblem problem)
        {
            throw new NotImplementedException();
        }
    }
}