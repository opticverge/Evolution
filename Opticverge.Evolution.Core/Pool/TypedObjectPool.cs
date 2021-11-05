using Microsoft.Extensions.ObjectPool;

namespace Opticverge.Evolution.Core.Pool
{
    /// <summary>
    /// Provides a way to inherit and customise a typed object pool
    /// </summary>
    /// <typeparam name="T">The inherting class</typeparam>
    /// <typeparam name="V">The type to be stored in the pool</typeparam>
    public abstract class TypedObjectPool<T, V> where V : class, new() where T : class, new()
    {
        private readonly ObjectPool<V> _pool;

        protected TypedObjectPool()
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
