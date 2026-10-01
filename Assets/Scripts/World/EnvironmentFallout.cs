using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace DefuseOrLose
{
    public class EnvironmentFallout : EnvironmentBase
    {
        private const float MinFrequencyOffset = -5f;
        private const float MaxFrequencyOffset = 5f;

        [SerializeField] private Color minLightColor;
        [SerializeField] private Color maxLightColor;
        [SerializeField] private float lightChangeSpeed;
        [SerializeField] private float corruptionFrequency;
        [SerializeField] private float corruptionDuration;
        [Header("Refs")]
        [SerializeField] private CanvasGroup tabletUI;
        [SerializeField] private GameObject falloutSound;

        private Color initialLightColor;
        private ScreenDistributor screenDistributor;
        private bool isCorrupted = false;

        void Start()
        {
            initialLightColor = globalLight.color;
            screenDistributor = GameController.Instance.ScreenDistributor;
            screenDistributor.PuzzleDisplayed += ApplyCorruptionToPuzzle;
            StartCoroutine(CorruptionRoutine());
        }

        void OnDestroy()
        {
            if (screenDistributor != null)
            {
                screenDistributor.PuzzleDisplayed -= ApplyCorruptionToPuzzle;
            }
        }

        private IEnumerator CorruptionRoutine()
        {
            while (true)
            {
                yield return WaitForRandomizedInterval(corruptionFrequency, MinFrequencyOffset, MaxFrequencyOffset);

                StartCorruption();
                AudioManager.Instance.PlaySound(falloutSound);
                yield return StartCoroutine(FadeLightColor(PickCorruptedLightColor()));

                yield return new WaitForSeconds(corruptionDuration);

                StopCorruption();
                yield return StartCoroutine(FadeLightColor(initialLightColor));
            }
        }

        private void StartCorruption()
        {
            isCorrupted = true;
            ApplyCorruption();
            CursorController.Instance.SetCursorDisabled();
        }

        private void StopCorruption()
        {
            isCorrupted = false;
            ApplyCorruption();
            CursorController.Instance.SetCursorEnabled();
        }

        private void ApplyCorruption()
        {
            ApplyCorruptionToCanvas(tabletUI);
            ApplyCorruptionToPuzzle(screenDistributor.ActivePuzzle);
        }

        private void ApplyCorruptionToPuzzle(GameObject puzzle)
        {
            if (puzzle == null)
                return;

            CanvasGroup puzzleUI = puzzle.GetComponentInChildren<CanvasGroup>();
            if (puzzleUI != null)
            {
                ApplyCorruptionToCanvas(puzzleUI);
            }
        }

        private void ApplyCorruptionToCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.interactable = !isCorrupted;
            canvasGroup.GetComponent<GraphicRaycaster>().enabled = !isCorrupted;
        }

        private Color PickCorruptedLightColor()
        {
            return new Color(
                Random.Range(minLightColor.r, maxLightColor.r),
                Random.Range(minLightColor.g, maxLightColor.g),
                Random.Range(minLightColor.b, maxLightColor.b)
            );
        }

        private IEnumerator FadeLightColor(Color targetColor)
        {
            Color startColor = globalLight.color;
            float elapsedTime = 0f;

            while (elapsedTime < lightChangeSpeed)
            {
                elapsedTime += Time.deltaTime;
                globalLight.color = Color.Lerp(startColor, targetColor, elapsedTime / lightChangeSpeed);
                yield return null;
            }

            globalLight.color = targetColor;
        }
    }
}
