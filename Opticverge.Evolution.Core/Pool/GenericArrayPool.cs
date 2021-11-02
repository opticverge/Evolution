using System.Buffers;

namespace Opticverge.Evolution.Core.Pool
{
    public class GenericArrayPool<T>
    {
        private readonly ArrayPool<T> _pool;

        private static readonly GenericArrayPool<T> _instance = new GenericArrayPool<T>();

        public static GenericArrayPool<T> Instance
        {
            get { return _instance; }
        }

        protected GenericArrayPool()
        {
            _pool = ArrayPool<T>.Create();
        }

        public T[] Rent(int minimumLength)
        {
            return _pool.Rent(minimumLength);
        }

        public void Return(T[] array, bool clearArray = false)
        {
            _pool.Return(array, clearArray);
        }
    }
}
