using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace PalTracker
{
    public class OperationCounter<T, E> : IOperationCounter<T, E>
    {
        private readonly IDictionary<E, int> trackedOperations;

        public OperationCounter()
        {
            trackedOperations = new Dictionary<E, int>();

            foreach (var op in Enum.GetValues(typeof(E)))
            {
                trackedOperations.Add((E) op, 0);   
            }
        }

        public IDictionary<E, int> GetCounts => trackedOperations.ToImmutableDictionary();

        public string Name => $"{typeof(T).Name}Operations";

        public void Increment(E operation)
        {
            trackedOperations[operation] = ++trackedOperations[operation];
        }
    }
}