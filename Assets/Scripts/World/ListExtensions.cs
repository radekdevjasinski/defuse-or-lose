using System.Collections.Generic;
using UnityEngine;

namespace DefuseOrLose
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> items)
        {
            for (int i = items.Count - 1; i > 0; i--)
            {
                int swapIndex = Random.Range(0, i + 1);
                T swapped = items[i];
                items[i] = items[swapIndex];
                items[swapIndex] = swapped;
            }
        }
    }
}
