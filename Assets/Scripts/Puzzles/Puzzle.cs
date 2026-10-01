using UnityEngine;

namespace DefuseOrLose
{
    public class Puzzle
    {
        public GameObject Prefab { get; }
        public bool IsCompleted { get; set; }

        public Puzzle(GameObject prefab)
        {
            Prefab = prefab;
        }
    }
}
