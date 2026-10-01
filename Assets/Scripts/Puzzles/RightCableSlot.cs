using UnityEngine;

namespace DefuseOrLose
{
    public class RightCableSlot : MonoBehaviour
    {
        [Tooltip("Zero-based: 0 is slot 1, 1 is slot 2.")]
        [SerializeField] private int rightSlotIndex;

        public int RightSlotIndex => rightSlotIndex;
        public bool IsUsed { get; set; }
    }
}
