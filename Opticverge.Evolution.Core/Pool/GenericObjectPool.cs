using Microsoft.Extensions.ObjectPool;

namespace Opticverge.Evolution.Core.Pool
{
    /// <summary>
    /// Generates a singleton per type that provides a reusable pool of objects for that type
    /// </summary>
    /// <typeparam name="T">The type stored in the pool</typeparam>
    public class GenericObjectPool<T> where T : class, new()
    {
        private readonly ObjectPool<T> _pool;

        protected GenericObjectPool()
        {
            _pool = new DefaultObjectPool<T>(new DefaultPooledObjectPolicy<T>(), MaximumRetained);
        }

        protected virtual int MaximumRetained => 1024;

        public static GenericObjectPool<T> Instance { get; } = new GenericObjectPool<T>();

        public virtual T Get()
        {
            return _pool.Get();
        }

        public virtual void Return(T obj)
        {
            _pool.Return(obj);
        }
    }
}
