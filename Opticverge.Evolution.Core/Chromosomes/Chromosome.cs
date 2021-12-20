using System;
using System.Text;
using OneOf;
using Opticverge.Evolution.Core.Generators;
using Opticverge.Evolution.Core.LifeCycle;
using Opticverge.Evolution.Core.Pool;

namespace Opticverge.Evolution.Core.Chromosomes
{
    /// <summary>
    /// A chromosome is the base class for all evolutionary processes.
    /// </summary>
    /// <remarks>
    /// The chromosome comes with a fast pseudo random number generator.
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class Chromosome<T> : IChromosome, ILifeCycle
    {
        public ulong? Seed { get; protected set; }

        public XorShiftPlusGenerator Generator { get; protected set; }

        public T Phenotype { get; protected set; }

        public StringBuilder IdBuilder { get; protected set; }

        public Chromosome(ulong? seed = null)
        {
            Seed = seed;
            Setup();
        }


        public TValue Generate<TValue>(
            OneOf<TValue, Chromosome<TValue>>? dna,
            TValue value
        )
        {
            if (dna == null) return value;

            if (!dna.Value.IsT1) return dna.Value.AsT0;

            dna.Value.AsT1.Generate();

            return dna.Value.AsT1.Phenotype;
        }

        public TValue Mutate<TValue>(
            OneOf<TValue, Chromosome<TValue>>? dna,
            TValue value
        )
        {
            if (dna == null) return value;

            if (!dna.Value.IsT1) return dna.Value.AsT0;

            dna.Value.AsT1.Mutate();

            return dna.Value.AsT1.Phenotype;
        }

        public virtual void Generate()
        {
            throw new NotImplementedException();
        }

        public virtual void Mutate()
        {
            throw new NotImplementedException();
        }

        public virtual void Setup()
        {
            Generator?.Reset();
            Generator ??= XorShiftPlusGeneratorPool.Instance.Get(Seed.GetValueOrDefault());

            IdBuilder?.Clear();
            IdBuilder ??= GenericObjectPool<StringBuilder>.Instance.Get();

            Phenotype = default(T);
        }

        public virtual void Teardown()
        {
        }
    }
}