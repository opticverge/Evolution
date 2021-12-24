using System;
using OneOf;

namespace Opticverge.Evolution.Core.Problems
{
    public class LifeTime : OneOfBase<ulong, TimeSpan, DateTime, Func<IProblem, bool>>
    {
        /// <summary>
        /// LifeTime is a period of time for which an evolutionary process is active.
        /// </summary>
        /// <typeparam ref="input" name="ulong">The number of generations before the evolutionary process terminates</typeparam>
        /// <typeparam ref="input" name="TimeSpan">The length of time before the evolutionary process terminates</typeparam>
        /// <typeparam ref="input" name="DateTime">The date and time before the evolutionary process terminates</typeparam>
        /// <typeparam ref="input" name="Func">A customisable function that terminates the evolutionary process</typeparam>
        /// <example>
        /// <code>
        /// // Terminate the evolutionary process after 100 generations
        /// var lifeTime = new LifeTime(100);
        /// <br />
        ///
        /// // Terminate the evolutionary process after 1 hour
        /// var lifeTime = new LifeTime(TimeSpan.FromHours(1));
        /// <br />
        ///
        /// // Terminate the evolutionary process at a future date
        /// var lifeTime = new LifeTime(new DateTime(2021, 1, 1));
        /// <br />
        ///
        /// // Terminate the evolutionary process when the fitness of the best chromosome satisfies the condition
        /// var lifeTime = new LifeTime(problem => problem.Best.Fitness > 0.9)
        /// </code>
        /// </example>
        public LifeTime(OneOf<ulong, TimeSpan, DateTime, Func<IProblem, bool>> input) :
            base(input)
        {
        }
    }
}
