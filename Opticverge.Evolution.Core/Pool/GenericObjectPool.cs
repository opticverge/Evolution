using Microsoft.Extensions.ObjectPool;

namespace Opticverge.Evolution.Core.Pool
{
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
