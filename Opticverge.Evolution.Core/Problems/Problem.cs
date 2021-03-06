using System;
using System.Threading;
using ConcurrentCollections;
using Opticverge.Evolution.Core.Chromosomes;
using Opticverge.Evolution.Core.Meta;

namespace Opticverge.Evolution.Core.Problems
{
    public class Problem : IProblem
    {
        private Func<IProblem, bool> _terminatingCondition;
        public CancellationTokenSource TokenSource { get; }
        public ulong Generation { get; private set; }
        public ulong Epochs { get; protected set; }
        public IChromosome[] Population { get; protected set; }
        public int PopulationSize { get; protected set; }
        public int Concurrency { get; }
        public ConcurrentHashSet<ulong> Generated { get; }
        public LifeTime LifeTime { get; }
        public Objective Objective { get; }
        public Summary Summary { get; }

        public Problem(
            Objective objective,
            LifeTime lifeTime = null,
            int populationSize = 100,
            int concurrency = 1,
            CancellationTokenSource tokenSource = default
        )
        {
            TokenSource = tokenSource ?? new CancellationTokenSource();
            lifeTime?.Switch(
                epochs => Epochs = epochs,
                timeSpan => TokenSource.CancelAfter(timeSpan),
                dateTime => TokenSource.CancelAfter((dateTime.Subtract(DateTime.UtcNow)).Milliseconds),
                terminatingCondition => _terminatingCondition = terminatingCondition
            );
            Objective = objective;
            LifeTime = lifeTime;

            Generated = new ConcurrentHashSet<ulong>();
            Concurrency = Math.Max(1, concurrency);
            PopulationSize = populationSize > 0
                ? populationSize
                : throw new ArgumentOutOfRangeException(nameof(populationSize),
                    $"{nameof(populationSize)} must be greater than 0");
            Population = new IChromosome[PopulationSize];
            Summary = new Summary();
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

                var chromosome = Create();
                chromosome.Generate();
                Population[i] = chromosome;
                Generated.Add(chromosome.Hash);
            }
        }

        public virtual IChromosome Create()
        {
            throw new NotImplementedException();
        }

        public void NextGeneration()
        {
            Generation++;

            if (Epochs != 0UL && Generation >= Epochs)
            {
                TokenSource.Cancel();
            }

            if (_terminatingCondition?.Invoke(this) == true)
            {
                TokenSource.Cancel();
            }
        }

        public void Start()
        {
            Summary.Start();
        }

        public void End()
        {
            Summary.End();
        }

        public void Evaluate()
        {
            throw new NotImplementedException();
        }

        public void Sort()
        {
            throw new NotImplementedException();
        }
    }
}
