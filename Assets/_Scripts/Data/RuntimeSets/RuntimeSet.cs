using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Data.RuntimeSets
{
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        public List<T> Items = new();

        public void Initialize()
        {
            Items.Clear();
        }

        public T GetItemIndex(int index)
        {
            return Items[index];
        }

        public void AddToList(T thingToAdd)
        {
            if (!Items.Contains(thingToAdd))
                Items.Add(thingToAdd);
        }

        public void RemoveFromList(T thingToRemove)
        {
            if (Items.Contains(thingToRemove))
                Items.Remove(thingToRemove);
        }
    }
}