using UnityEngine;
using UnityEngine.EventSystems;

public class RightCableSlot : MonoBehaviour, IDropHandler
{
    [HideInInspector] public bool isUsed = false;
    public int rightSlotIndex;  // Ustaw w Inspectorze (0 = slot 1, 1 = slot 2, itd.)

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("[RightCableSlot] OnDrop: Slot dropped on " + gameObject.name);
    }
}
