using System.Collections;
using System.Collections.Generic;

namespace FastTypes.DataStructures
{
    internal sealed class LockableList<T> : IReadOnlyList<T>, ICollection<T>
    {
        private readonly List<T> _listImplementation;

        public LockableList()
        {
            _listImplementation = new List<T>();
        }

        public void Add(T value)
        {
            lock (_listImplementation)
            {
                _listImplementation.Add(value);
            }
        }

        public void Clear()
        {
            lock (_listImplementation)
            {
                _listImplementation.Clear();
            }
        }

        public bool Contains(T item)
        {
            return _listImplementation.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_listImplementation)
            {
                _listImplementation.CopyTo(array, arrayIndex);
            }
        }

        public bool Remove(T item)
        {
            lock (_listImplementation)
            {
                return _listImplementation.Remove(item);
            }
        }

        public void AddRange(IEnumerable<T> values)
        {
            lock (_listImplementation)
            {
                _listImplementation.AddRange(values);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _listImplementation.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_listImplementation).GetEnumerator();
        }

        public int Count => _listImplementation.Count;
        public bool IsReadOnly => false;
        public T this[int index] => _listImplementation[index];
    }
}