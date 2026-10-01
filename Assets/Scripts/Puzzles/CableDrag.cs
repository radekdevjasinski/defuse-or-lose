using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace DefuseOrLose
{
    public class CableDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject cablePrefab;
        [SerializeField] private int leftSlotIndex;

        private Cable currentCable;
        private Canvas canvas;
        private RectTransform canvasRect;

        void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
            canvasRect = canvas.GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (CablePuzzleManager.Instance.IsLeftSlotLocked(leftSlotIndex))
                return;

            currentCable = Instantiate(cablePrefab, canvas.transform).GetComponent<Cable>();
            currentCable.SetStart(transform.position);
            currentCable.SetEnd(transform.position);
            CursorController.Instance.SetCursorHold();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (currentCable == null)
                return;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
                return;

            Rect rect = canvasRect.rect;
            localPoint.x = Mathf.Clamp(localPoint.x, rect.xMin, rect.xMax);
            localPoint.y = Mathf.Clamp(localPoint.y, rect.yMin, rect.yMax);
            currentCable.SetEnd(canvasRect.TransformPoint(localPoint));
            CursorController.Instance.SetCursorHold();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (currentCable == null)
                return;

            if (TryFindFreeSlot(eventData, out RightCableSlot slot))
            {
                currentCable.SetEnd(slot.transform.position);
                slot.IsUsed = true;
                CablePuzzleManager.Instance.RegisterConnection(leftSlotIndex, slot.RightSlotIndex);
            }
            else
            {
                Destroy(currentCable.gameObject);
            }

            currentCable = null;
            CursorController.Instance.SetCursorDefault();
        }

        private static bool TryFindFreeSlot(PointerEventData eventData, out RightCableSlot freeSlot)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (RaycastResult result in results)
            {
                RightCableSlot slot = result.gameObject.GetComponent<RightCableSlot>();
                if (slot != null && !slot.IsUsed)
                {
                    freeSlot = slot;
                    return true;
                }
            }

            freeSlot = null;
            return false;
        }
    }
}
