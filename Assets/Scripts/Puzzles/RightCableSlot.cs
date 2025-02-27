using UnityEngine;
using UnityEngine.EventSystems;

public class RightCableSlot : MonoBehaviour, IDropHandler
{
    [HideInInspector] public bool isUsed = false;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("[RightCableSlot] OnDrop: Slot dropped on.");
    }
}
