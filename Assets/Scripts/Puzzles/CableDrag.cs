using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CableDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject cablePrefab;
    private Cable currentCable;
    private Canvas canvas;
    public int leftSlotIndex;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            //Debug.LogError("[CableDrag] Start: Canvas not found!");
        }
        else
        {
            //Debug.Log("[CableDrag] Start: Canvas found - " + canvas.name);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CablePuzzleManager.Instance != null && CablePuzzleManager.Instance.IsLeftSlotLocked(leftSlotIndex))
        {
            //Debug.LogWarning($"[CableDrag] OnBeginDrag: Left slot {leftSlotIndex + 1} is already locked. Drag aborted.");
            return;
        }

        //Debug.Log("[CableDrag] OnBeginDrag: Started dragging " + gameObject.name);
        if (canvas == null)
        {
            //Debug.LogError("[CableDrag] OnBeginDrag: Canvas is null!");
            return;
        }
        GameObject cableObj = Instantiate(cablePrefab, canvas.transform);
        currentCable = cableObj.GetComponent<Cable>();
        if (currentCable == null)
        {
            //Debug.LogError("[CableDrag] OnBeginDrag: Cable prefab not found!");
            return;
        }
        RectTransform rt = GetComponent<RectTransform>();
        currentCable.startPos = rt.position;
        currentCable.endPos = rt.position;
        //Debug.Log("[CableDrag] OnBeginDrag: Cable created, startPos = " + currentCable.startPos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentCable != null)
        {
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, eventData.position, eventData.pressEventCamera, out localPoint))
            {
                Rect rect = canvasRect.rect;
                localPoint.x = Mathf.Clamp(localPoint.x, rect.xMin, rect.xMax);
                localPoint.y = Mathf.Clamp(localPoint.y, rect.yMin, rect.yMax);
                Vector3 clampedWorldPos = canvasRect.TransformPoint(localPoint);
                currentCable.endPos = clampedWorldPos;
                //Debug.Log("[CableDrag] OnDrag: endPos (clamped) " + clampedWorldPos);
            }
            else
            {
                //Debug.LogWarning("[CableDrag] OnDrag: Error converting mouse position!");
            }
        }
    }

public void OnEndDrag(PointerEventData eventData)
{
    if (currentCable == null)
        return;

    List<RaycastResult> results = new List<RaycastResult>();
    EventSystem.current.RaycastAll(eventData, results);
    bool foundSlot = false;
    foreach (RaycastResult result in results)
    {
        RightCableSlot slot = result.gameObject.GetComponent<RightCableSlot>();
        if (slot != null && !slot.isUsed)
        {
            RectTransform slotRT = result.gameObject.GetComponent<RectTransform>();
            if (slotRT != null)
            {
                currentCable.endPos = slotRT.position;
                slot.isUsed = true;
                foundSlot = true;
                if (CablePuzzleManager.Instance != null)
                {
                    CablePuzzleManager.Instance.RegisterConnection(this.leftSlotIndex, slot.rightSlotIndex);
                }
                break;
            }
        }
    }
    if (!foundSlot)
    {
        if (currentCable.gameObject != null)
        {
            Destroy(currentCable.gameObject);
        }
    }
    currentCable = null;
}
}
