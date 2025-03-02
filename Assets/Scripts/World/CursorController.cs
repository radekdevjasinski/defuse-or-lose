using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorController : MonoBehaviour
{
    public static CursorController instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        //SetCursorDefault();
    }
    public Texture2D[] cursorTextures;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hand;
    public Vector2 hold;
    public void SetCursorDefault()
    {
        Cursor.SetCursor(cursorTextures[0], Vector2.zero, cursorMode);
    }
    public void SetCursorHand()
    {
        Cursor.SetCursor(cursorTextures[1], hand, cursorMode);
    }
    public void SetCursorHold()
    {
        Cursor.SetCursor(cursorTextures[2], hold, cursorMode);
    }
}

