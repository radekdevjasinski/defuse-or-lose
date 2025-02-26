using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Vector3 targetPosition; // Docelowa pozycja kamery
    public float targetSize;  // Docelowa wielkość kamery
    public float duration;    // Czas animacji

    private Camera cam;
    private Vector3 startPosition;
    private float startSize;
    private bool isAnimating = false;
    private bool toggled = false; // Czy kamera jest w pozycji docelowej?

    void Start()
    {
        cam = Camera.main;
        startPosition = cam.transform.position;
        startSize = cam.orthographicSize;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAnimating)
        {
            Vector3 newPosition = toggled ? startPosition : targetPosition;
            float newSize = toggled ? startSize : targetSize;

            StartCoroutine(AnimateCamera(newPosition, newSize));
            toggled = !toggled; // Zmiana stanu
        }
    }

    IEnumerator AnimateCamera(Vector3 newPosition, float newSize)
    {
        isAnimating = true;
        float elapsed = 0f;
        Vector3 initialPosition = cam.transform.position;
        float initialSize = cam.orthographicSize;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            cam.transform.position = Vector3.Lerp(initialPosition, newPosition, t);
            cam.orthographicSize = Mathf.Lerp(initialSize, newSize, t);
            yield return null;
        }

        cam.transform.position = newPosition;
        cam.orthographicSize = newSize;
        isAnimating = false;
    }
}
