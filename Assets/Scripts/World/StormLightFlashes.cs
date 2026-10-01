using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace DefuseOrLose
{
    public class StormLightFlashes : MonoBehaviour
    {
        [SerializeField] private Light2D stormLight;
        [SerializeField] private float minTimeBetweenFlashes = 2f;
        [SerializeField] private float maxTimeBetweenFlashes = 10f;
        [SerializeField] private float flashDuration = 0.1f;
        [SerializeField] private float minIntensity = 0.5f;
        [SerializeField] private float maxIntensity = 2f;

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
}
