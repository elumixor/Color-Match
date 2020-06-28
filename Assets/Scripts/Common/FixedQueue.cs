using System.Collections;
using System.Collections.Generic;

namespace Common {
    public struct FixedQueue<T> : IEnumerable<T> where T : class {
        private readonly T[] items;
        private readonly int maxSize;
        private int size;
        private int ptr;

        public FixedQueue(int maxSize) {
            this.maxSize = maxSize;
            items = new T[maxSize];
            size = 0;
            ptr = 0;
        }

        public void Enqueue(T item) {
            items[ptr] = item;
            ptr = (ptr + 1) % maxSize;
            if (size < maxSize) size++;
        }

        public bool Contains(T item) {
            for (var i = 0; i < size; i++)
                if (item == items[i])
                    return true;
            return false;
        }

        public IEnumerator<T> GetEnumerator() {
            for (var i = 0; i < size; i++)
                yield return items[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}