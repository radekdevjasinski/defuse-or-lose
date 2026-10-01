using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefuseOrLose
{
    public class CablePuzzleManager : PuzzleBase
    {
        public static CablePuzzleManager Instance { get; private set; }

        private readonly Dictionary<int, int> actualConnections = new Dictionary<int, int>();
        private Dictionary<int, int> expectedConnections = new Dictionary<int, int>();

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            Initialize();
        }

        public override void Initialize()
        {
            BombController bomb = BombController.Instance;
            if (!CableRuleBook.TryCalculateConnections(bomb.SerialCode, bomb.BatteryBars, out expectedConnections))
            {
                Debug.LogError("CablePuzzleManager: cable rules collide for this bomb, the puzzle cannot be solved.");
            }

            EditorLog.Log("Expected cable connections: " + string.Join(", ",
                expectedConnections.Select(connection => $"{connection.Key + 1} -> {connection.Value + 1}")));
        }

        public bool IsLeftSlotLocked(int leftIndex)
        {
            return actualConnections.ContainsKey(leftIndex);
        }

        public void RegisterConnection(int leftIndex, int rightIndex)
        {
            if (IsLeftSlotLocked(leftIndex))
                return;

            if (expectedConnections[leftIndex] != rightIndex)
            {
                OnFail();
                return;
            }

            actualConnections[leftIndex] = rightIndex;
            if (actualConnections.Count == CableRuleBook.LeftSlotCount)
            {
                OnComplete();
            }
        }
    }
}
