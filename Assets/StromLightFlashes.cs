using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StormLightFlashes : MonoBehaviour
{
    public Light2D stormLight;
    public float minTimeBetweenFlashes = 2f;
    public float maxTimeBetweenFlashes = 10f;
    public float flashDuration = 0.1f;
    public float minIntensity = 0.5f;
    public float maxIntensity = 2f;

    private void Start()
    {
        if (stormLight == null)
            stormLight = GetComponent<Light2D>();

        stormLight.intensity = 0f;
        StartCoroutine(StormFlashRoutine());
    }

    private IEnumerator StormFlashRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBetweenFlashes, maxTimeBetweenFlashes));
            yield return StormFlash();
        }
    }

    private IEnumerator StormFlash()
    {
        stormLight.intensity = Random.Range(minIntensity, maxIntensity);
        yield return new WaitForSeconds(flashDuration);
        stormLight.intensity = 0f;
    }
}
