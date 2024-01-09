using System.Collections;
using System.Collections.Generic;

namespace FastTypes.DataStructures
{
    internal sealed class LockableList<T> : IReadOnlyList<T>, ICollection<T>
    {
        private readonly List<T> _lst;

        public LockableList()
        {
            _lst = new List<T>();
        }

        public void Add(T value)
        {
            lock (_lst)
            {
                _lst.Add(value);
            }
        }

        public void Clear()
        {
            lock (_lst)
            {
                _lst.Clear();
            }
        }

        public bool Contains(T item) => _lst.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_lst)
            {
                _lst.CopyTo(array, arrayIndex);
            }
        }

        public bool Remove(T item)
        {
            lock (_lst)
            {
                return _lst.Remove(item);
            }
        }

        public void AddRange(IEnumerable<T> values)
        {
            lock (_lst)
            {
                _lst.AddRange(values);
            }
        }

        public IEnumerator<T> GetEnumerator() => _lst.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_lst).GetEnumerator();

        public int Count => _lst.Count;
        public bool IsReadOnly => false;
        public T this[int index] => _lst[index];
    }
}