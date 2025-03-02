using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CableDrag : PuzzleBase, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject cablePrefab;
    private Cable currentCable;
    private Canvas canvas;
    public int leftSlotIndex;

    public override void Initialize()
    {
        canvas = GetComponentInParent<Canvas>();
    }
    void Start()
    {
        Initialize();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CablePuzzleManager.Instance != null && CablePuzzleManager.Instance.IsLeftSlotLocked(leftSlotIndex))
            return;

        if (canvas == null)
            return;

        GameObject cableObj = Instantiate(cablePrefab, canvas.transform);
        currentCable = cableObj.GetComponent<Cable>();
        if (currentCable == null)
            return;

        RectTransform rt = GetComponent<RectTransform>();
        currentCable.startPos = rt.position;
        currentCable.endPos = rt.position;
        CursorController.instance.SetCursorHold();
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
                CursorController.instance.SetCursorHold();
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
            Destroy(currentCable.gameObject);
        }

        currentCable = null;
        CursorController.instance.SetCursorDefault();
    }


}
