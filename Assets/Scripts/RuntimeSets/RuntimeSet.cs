using System.Collections.Generic;
using UnityEngine;

namespace RuntimeSets
{
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        private readonly List<T> items = new ();

        public virtual void Initialize()
            => items.Clear();

        public virtual T GetItem(int index)
            => items[index];
        
        public virtual void Add(T thing)
        {
            if (!items.Contains(thing))
            {
                items.Add(thing);
            }
        }

        public virtual void Remove(T thing)
        {
            if (items.Contains(thing))
            {
                items.Remove(thing);
            }
        }
    }
}