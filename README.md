[![.NET](https://github.com/opticverge/Evolution/actions/workflows/dotnet.yml/badge.svg)](https://github.com/opticverge/Evolution/actions/workflows/dotnet.yml)

# Introduction
Evolution is a discovery library that provides a framework for configurable and compositional evolutionary search for combinatoric exploration. 

# Installation
TBC

# Features

## Memory Management
In order to optimise throughput, pools are utilised for the creation of objects and arrays. The evolutionary process may create millions of unique chromosomes per second but there is an upperbound on the total number of active chromosomes per generation. 

For example, with the `ArtificialImmuneSystem` the default mutation strategy will create `n * m` clones where `n` is the size of the population and `m` is the number of clones to create per chromosome. At any given time a maximum of `n * m` clones are required throughout the evolutionary lifecycle.  

## Performance
Memory management contributes to improvements in performance here with reductions in memory allocations through the reuse of objects and arrays. This approach increases our throughput by approximately 2 orders of magnitude. For example, if it takes 2000ns to create a chromosome the first time, with the use of pools it would only take 25ns to fetch a chromosome from the pool and approximately 10ns to reset the chromosome. This means we average from around 500K chromosomes per second to 28M chromosomes per second with pooling+.    

A fast pseudo-random number generator is used which was measured against several other libraries to be the most performant. 

The Task Parallel Library (TPL) is used to maximise the concurrency of the evolutionary process, especially during the mutation phase of the algorithm. TPL outperformed other forms of concurrency in C#.

## Control
We have provided three core evolutionary control mechanisms for problem solving. These are Lifetime, Concurrency and Cancellation. 

### Lifetime Control
Search typically has a lifetime. In `Evolution` there are several lifetime options available that are integrated into the evolutionary lifecycle. 
- **Null** *(Default)*. The algorithm will run indefinitely until the user cancels it.
- **Generations**. The algorithm will run for a fixed number of generations. e.g. `new Lifetime(10)` 
- **TimeSpan**. The algorithm will run for a fixed amount of time in milliseconds, seconds, minutes, hours, days, weeks, months or years. e.g. `new LifeTime(TimeSpan.FromSeconds(10))`.  
- **DateTime**. The algorithm will run until a fixed date and time. e.g. `new LifeTime(new DateTime(2021, 1, 1))`.  
- **Function**. The algorithm will run until a customisable function returns true. e.g. `new LifeTime(problem => problem.Best.Fitness > 0.98)`.

### Concurrency
Since each environment is different you can control the concurrency of the evolutionary process. This affects the mutation phase of the algorithm significantly. 

### Cancellation
`Evolution` is engineered to take cancellation into account. At the core of all  components of the evolutionary process is the `CancellationTokenSource`. The `CancellationToken` attached to the token source is distributed to all processes that require termination when the token is cancelled.   
