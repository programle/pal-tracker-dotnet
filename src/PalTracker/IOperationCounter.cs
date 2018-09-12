﻿using System.Collections.Generic;

namespace PalTracker
{
    public interface IOperationCounter<T, E>
    {
        void Increment(E operation);

        IDictionary<E, int> GetCounts { get; }

        string Name { get; }
    }
}