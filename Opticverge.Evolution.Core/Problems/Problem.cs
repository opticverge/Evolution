using System;
using System.Threading;
using ConcurrentCollections;
using Opticverge.Evolution.Core.Chromosomes;

namespace Opticverge.Evolution.Core.Problems
{
    public class Problem : IProblem
    {
        private Func<IProblem, bool> _terminatingCondition;
        public CancellationTokenSource TokenSource { get; }
        public ulong Generation { get; protected set; }
        public ulong Epochs { get; protected set; }
        public IChromosome[] Population { get; protected set; }
        public int PopulationSize { get; protected set; }
        public int Concurrency { get; }
        public ConcurrentHashSet<ulong> Generated { get; protected set; }
        public LifeTime LifeTime { get; }

        public Problem(
            LifeTime lifeTime = null,
            int populationSize = 100,
            int concurrency = 1,
            CancellationTokenSource tokenSource = default
        )
        {
            TokenSource = tokenSource ?? new CancellationTokenSource();

            LifeTime = lifeTime;
            LifeTime?.Switch(
                epochs => Epochs = epochs,
                timeSpan => TokenSource.CancelAfter(timeSpan),
                dateTime => TokenSource.CancelAfter((dateTime.Subtract(DateTime.UtcNow)).Milliseconds),
                terminatingCondition => _terminatingCondition = terminatingCondition
            );

            Generated = new ConcurrentHashSet<ulong>();
            Concurrency = Math.Max(1, concurrency);
            PopulationSize = populationSize > 0
                ? populationSize
                : throw new ArgumentOutOfRangeException(nameof(populationSize));
            Population = new IChromosome[PopulationSize];
        }


        public void Initialise()
        {
            for (var i = 0; i < PopulationSize; i++)
            {
                TokenSource.Token.ThrowIfCancellationRequested();

                if (Population[i] != null)
                {
                    continue;
                }

                Population[i] = Generate();
                Population[i].Generate();
                Generated.Add(Population[i].Hash);
            }
        }

        public virtual IChromosome Generate()
        {
            throw new NotImplementedException();
        }
    }
}