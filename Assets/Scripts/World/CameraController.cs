using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace DefuseOrLose
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 targetPosition;
        [SerializeField] private float targetSize;
        [SerializeField] private float duration;

        private Camera cam;
        private Vector3 startPosition;
        private float startSize;
        private Vector3 basePosition;
        private bool isAnimating = false;
        private bool isZoomedIn = false;

        public Vector3 ShakeOffset { get; set; }

        void Start()
        {
            cam = Camera.main;
            startPosition = cam.transform.position;
            startSize = cam.orthographicSize;
            basePosition = startPosition;
        }

        void Update()
        {
            if (isAnimating || !WasZoomKeyPressed())
                return;

            Vector3 newPosition = isZoomedIn ? startPosition : targetPosition;
            float newSize = isZoomedIn ? startSize : targetSize;

            StartCoroutine(AnimateCamera(newPosition, newSize));
            isZoomedIn = !isZoomedIn;
        }

        void LateUpdate()
        {
            cam.transform.position = basePosition + ShakeOffset;
        }

        static bool WasZoomKeyPressed()
        {
            return Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;
        }

        IEnumerator AnimateCamera(Vector3 newPosition, float newSize)
        {
            isAnimating = true;
            float elapsed = 0f;
            Vector3 initialPosition = basePosition;
            float initialSize = cam.orthographicSize;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                basePosition = Vector3.Lerp(initialPosition, newPosition, t);
                cam.orthographicSize = Mathf.Lerp(initialSize, newSize, t);
                yield return null;
            }

            basePosition = newPosition;
            cam.orthographicSize = newSize;
            isAnimating = false;
        }
    }
}
