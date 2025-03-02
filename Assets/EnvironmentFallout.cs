using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class EnvironmentFallout : EnvironmentBase
{
    [SerializeField] private Color minLightColor;
    [SerializeField] private Color maxLightColor;
    private Color initialLightColor;
    [SerializeField] private float lightChangeSpeed;
    [SerializeField] private float corruptionFrequency;
    [SerializeField] private float corruptionDuration;
    [SerializeField] private float displayCorruptionDuration;
    [Header("Refs")]
    [SerializeField] private CanvasGroup tabletUI;
    [SerializeField] private GameObject puzzleHolder;


    protected override void Start()
    {
        base.Start();
        initialLightColor = globalLight.color;
        StartCoroutine(CorruptionRoutine());
    }
    private void Corrupt(bool state)
    {
        tabletUI.interactable = !state;
        CanvasGroup puzzleUI = puzzleHolder.GetComponentInChildren<CanvasGroup>();
        if(puzzleUI != null)
        {
            puzzleUI.interactable = !state;
        }
    }
    private IEnumerator CorruptionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(corruptionFrequency + Random.Range(-3f, 3f));

            Corrupt(true);
            CursorController.instance.SetCursorDisabled();
            yield return StartCoroutine(CorruptDisplay());

            yield return new WaitForSeconds(corruptionDuration);

            Corrupt(false);
            CursorController.instance.SetCursorEnabled();
            yield return StartCoroutine(RevertLightSettings());
        }
    }


    private IEnumerator CorruptDisplay()
    {
        Color startColor = globalLight.color;
        Color targetColor = new Color(
            Random.Range(minLightColor.r, maxLightColor.r),
            Random.Range(minLightColor.g, maxLightColor.g),
            Random.Range(minLightColor.b, maxLightColor.b)
        );
        float elapsedTime = 0f;

        while (elapsedTime < lightChangeSpeed)
        {
            elapsedTime += Time.deltaTime;
            globalLight.color = Color.Lerp(startColor, targetColor, elapsedTime / lightChangeSpeed);
            yield return null;
        }

        globalLight.color = targetColor;
    }

    private IEnumerator RevertLightSettings()
    {
        Color startColor = globalLight.color;
        float elapsedTime = 0f;

        while (elapsedTime < lightChangeSpeed)
        {
            elapsedTime += Time.deltaTime;
            globalLight.color = Color.Lerp(startColor, initialLightColor, elapsedTime / lightChangeSpeed);
            yield return null;
        }

        globalLight.color = initialLightColor;
    }
}
