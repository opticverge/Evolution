using Microsoft.Extensions.ObjectPool;

namespace Opticverge.Evolution.Core.Pool
{
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