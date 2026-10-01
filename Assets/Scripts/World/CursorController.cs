using UnityEngine;

namespace DefuseOrLose
{
    public class CursorController : MonoBehaviour
    {
        private const int DefaultTextureIndex = 0;
        private const int HandTextureIndex = 1;
        private const int HoldTextureIndex = 2;
        private const int DisabledTextureIndex = 3;

        public static CursorController Instance { get; private set; }

        [SerializeField] private Texture2D[] cursorTextures;
        [SerializeField] private CursorMode cursorMode = CursorMode.Auto;
        [SerializeField] private Vector2 hand;
        [SerializeField] private Vector2 hold;
        [SerializeField] private Vector2 disabled;

        private bool isDisabledMode = false;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetCursorDefault()
        {
            ApplyCursor(DefaultTextureIndex, Vector2.zero);
        }

        public void SetCursorHand()
        {
            ApplyCursor(HandTextureIndex, hand);
        }

        public void SetCursorHold()
        {
            ApplyCursor(HoldTextureIndex, hold);
        }

        public void SetCursorDisabled()
        {
            isDisabledMode = true;
            ApplyCursor(DisabledTextureIndex, disabled);
        }

        public void SetCursorEnabled()
        {
            isDisabledMode = false;
            SetCursorDefault();
        }

        private void ApplyCursor(int textureIndex, Vector2 hotspot)
        {
            if (isDisabledMode)
            {
                Cursor.SetCursor(cursorTextures[DisabledTextureIndex], disabled, cursorMode);
                return;
            }
            Cursor.SetCursor(cursorTextures[textureIndex], hotspot, cursorMode);
        }
    }
}
