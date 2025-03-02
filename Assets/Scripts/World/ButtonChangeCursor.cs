using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonChangeCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorController.instance.SetCursorHand();
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorController.instance.SetCursorDefault();
    }

}