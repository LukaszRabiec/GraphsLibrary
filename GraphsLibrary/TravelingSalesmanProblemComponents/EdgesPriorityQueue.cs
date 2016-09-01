using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphsLibrary.GraphComponents;

namespace GraphsLibrary.TravelingSalesmanProblemComponents
{
    public class EdgesPriorityQueue
    {
        public bool IsEmpty => _length == 0;

        private readonly SortedDictionary<int, Queue> _priorityQueue;
        private readonly object _mutex;
        private int _length;

        public EdgesPriorityQueue()
        {
            _priorityQueue = new SortedDictionary<int, Queue>();
            _length = 0;
            _mutex = new object();
        }

        public void Enqueue(Edge edge)
        {
            lock (_mutex)
            {
                if (!_priorityQueue.ContainsKey(edge.Cost))
                {
                    _priorityQueue.Add(edge.Cost, new Queue());
                }

                _priorityQueue[edge.Cost].Enqueue(edge);
            }

            _length++;
        }

        public Edge Dequeue()
        {
            ValidateIfQueueIsEmpty();

            var first = new Edge(0, 0, 0);

            lock (_mutex)
            {
                var notRemoved = true;

                while (notRemoved)
                {
                    if (_priorityQueue.First().Value.Count == 0)
                    {
                        _priorityQueue.Remove(_priorityQueue.First().Key);
                    }
                    else
                    {
                        first = (Edge)_priorityQueue.First().Value.Dequeue();
                        notRemoved = false;
                    }
                }
            }

            _length--;

            return first;
        }

        public Edge Peek()
        {
            ValidateIfQueueIsEmpty();

            lock (_mutex)
            {
                return (Edge)_priorityQueue.First(q => q.Value.Count != 0).Value.Peek();
            }
        }

        private void ValidateIfQueueIsEmpty()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Queue don't contains elements.");
            }
        }
    }
}
