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
    public Vector2 disabled;
    private bool disabledMode = false;

    public void SetCursorDefault()
    {
        if (disabledMode)
        {
            SetCursorDisabled();
            return;
        }
        Cursor.SetCursor(cursorTextures[0], Vector2.zero, cursorMode);
    }
    public void SetCursorHand()
    {
        if (disabledMode)
        {
            SetCursorDisabled();
            return;
        }
        Cursor.SetCursor(cursorTextures[1], hand, cursorMode);
    }
    public void SetCursorHold()
    {
        if (disabledMode)
        {
            SetCursorDisabled();
            return;
        }
        Cursor.SetCursor(cursorTextures[2], hold, cursorMode);
    }
    public void SetCursorDisabled()
    {
        disabledMode = true;
        Cursor.SetCursor(cursorTextures[3], disabled, cursorMode);
    }
    public void SetCursorEnabled()
    {
        disabledMode = false;
        SetCursorDefault();
    }
}

