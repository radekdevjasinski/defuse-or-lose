using System.Collections.Generic;

namespace DefuseOrLose
{
    public class CableWiring
    {
        private readonly Dictionary<int, int> connections = new Dictionary<int, int>();
        private readonly HashSet<int> usedRightSlots = new HashSet<int>();

        public Dictionary<int, int> Connections => connections;

        public bool IsLeftSlotAssigned(int leftSlot)
        {
            return connections.ContainsKey(leftSlot);
        }

        public bool IsRightSlotUsed(int rightSlot)
        {
            return usedRightSlots.Contains(rightSlot);
        }

        public void Connect(int leftSlot, int rightSlot)
        {
            connections[leftSlot] = rightSlot;
            usedRightSlots.Add(rightSlot);
        }
    }
}
