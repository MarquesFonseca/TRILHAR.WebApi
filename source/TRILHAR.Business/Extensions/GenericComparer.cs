namespace TRILHAR.Business.Extensions
{
    public class GenericComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _equalsFunc;
        private readonly Func<T, int> _hashCodeFunc;

        public GenericComparer(Func<T, T, bool> equalsFunc, Func<T, int> hashCodeFunc)
        {
            _equalsFunc = equalsFunc ?? throw new ArgumentNullException(nameof(equalsFunc));
            _hashCodeFunc = hashCodeFunc ?? throw new ArgumentNullException(nameof(hashCodeFunc));
        }

        public bool Equals(T x, T y) => _equalsFunc(x, y);

        public int GetHashCode(T obj) => _hashCodeFunc(obj);
    }
}
