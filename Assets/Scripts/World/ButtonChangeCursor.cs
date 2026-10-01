using UnityEngine;
using UnityEngine.EventSystems;

namespace DefuseOrLose
{
    public class ButtonChangeCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private bool isHovered;

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovered = true;
            CursorController.Instance.SetCursorHand();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHovered = false;
            CursorController.Instance.SetCursorDefault();
        }

        void OnDisable()
        {
            if (!isHovered || CursorController.Instance == null)
                return;

            isHovered = false;
            CursorController.Instance.SetCursorDefault();
        }
    }
}
