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

    public abstract class GenericObjectPool<T, V> where V : class, new() where T : class, new()
    {
        private readonly ObjectPool<V> _pool;

        protected GenericObjectPool()
        {
            _pool = new DefaultObjectPool<V>(
                new DefaultPooledObjectPolicy<V>(),
                MaximumRetained
            );
        }

        protected virtual int MaximumRetained => 1024;

        public static T Instance { get; } = new T();

        public virtual V Get()
        {
            return _pool.Get();
        }

        public virtual void Return(V obj)
        {
            _pool.Return(obj);
        }
    }
}
