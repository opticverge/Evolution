using System.Threading;
using ConcurrentCollections;
using Opticverge.Evolution.Core.Chromosomes;

namespace Opticverge.Evolution.Core.Problems
{
    public interface IProblem
    {
        /// <summary>
        /// Used to terminate the process of solving a problem
        /// </summary>
        public CancellationTokenSource TokenSource { get; }

        /// <summary>
        /// Indicates the current generation of solving the problem
        /// </summary>
        public ulong Generation { get; }

        /// <summary>
        /// Represents the current chromosomes who have a fitness that allows
        /// them to stay in the population during the problem solving process
        /// </summary>
        public IChromosome[] Population { get; }

        /// <summary>
        /// The maximum number of chromosomes in the population
        /// </summary>
        public int PopulationSize { get; }

        /// <summary>
        /// The total number of concurrent threads that will be used to solve the problem
        /// </summary>
        public int Concurrency { get; }

        /// <summary>
        /// Stores the hash of the Id of each generated chromosome
        /// </summary>
        public ConcurrentHashSet<ulong> Generated { get; }

        /// <summary>
        /// Initialises the <see cref="Population"/> using the <see cref="PopulationSize"/>
        /// </summary>
        public void Initialise();

        /// <summary>
        /// Generates a new <see cref="IChromosome"/>
        /// </summary>
        /// <returns></returns>
        public IChromosome Generate();

        /// <summary>
        /// Represents the maximum number of generations a problem will be solved for
        /// </summary>
        public ulong Epochs { get; }

        /// <summary>
        /// Increments the current generation to the next
        /// </summary>
        public void NextGeneration();

        /// <summary>
        /// A hook provided for when the evolutionary process starts
        /// </summary>
        void Start();

        /// <summary>
        /// Evaluates the population
        /// </summary>
        void Evaluate();

        /// <summary>
        /// A hook provided for when the evolutionary process ends
        /// </summary>
        void End();

        /// <summary>
        /// Sorts the population
        /// </summary>
        void Sort();
    }
}
