using System;
using System.Collections;
using System.Collections.Generic;

namespace FastTypes.DataStructures
{
    internal sealed class LockableSet<T> : IReadOnlyList<T>, ICollection<T>
    {
        private readonly List<T> _lst;
        private readonly Dictionary<T, int> _dic;

        public LockableSet()
        {
            _lst = new List<T>();
            _dic = new Dictionary<T, int>();
        }

        public void Add(T value)
        {
            lock (_lst)
            {
                if (_dic.TryAdd(value, _lst.Count))
                {
                    _lst.Add(value);
                }
            }
        }

        public void Clear()
        {
            lock (_lst)
            {
                _lst.Clear();
                _dic.Clear();
            }
        }

        public bool Contains(T item) => _dic.ContainsKey(item);

        public void CopyTo(T[] array, int arrayIndex) => throw new NotImplementedException();

        public bool Remove(T item)
        {
            lock (_lst)
            {
                return _lst.Remove(item) && _dic.Remove(item);
            }
        }

        public void AddRange(IEnumerable<T> values)
        {
            lock (_lst)
            {
                foreach (T value in values)
                {
                    Add(value);
                }
            }
        }

        public IEnumerator<T> GetEnumerator() => _lst.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_lst).GetEnumerator();

        public int Count => _lst.Count;
        public bool IsReadOnly => false;
        public T this[int index] => _lst[index];
    }
}