using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    internal sealed class PriorityQueue<TElement, TPriority> : IEnumerable<TElement>
        where TPriority : IComparable
    {
        private readonly SortedDictionary<TPriority, Queue<TElement>> _container;

        public int Count { get; private set; }

        public PriorityQueue()
        {
            _container = new SortedDictionary<TPriority, Queue<TElement>>();
        }

        public void Enqueue(TElement value, TPriority priority)
        {
            if (!_container.TryGetValue(priority, out var queue))
                _container[priority] = queue = new Queue<TElement>();
            queue.Enqueue(value);

            Count++;
        }

        public TElement Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("PriorityQueue is empty");

            var queue = _container.First();
            var element = queue.Value.Dequeue();

            if (queue.Value.Count == 0)
                _container.Remove(queue.Key);

            Count--;

            return element;
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            foreach (var pair in _container)
            {
                while (pair.Value.Count > 0)
                    yield return pair.Value.Dequeue();
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
