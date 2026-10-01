using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace DefuseOrLose
{
    public class EnvironmentStorm : EnvironmentBase
    {
        private const float MinFlashIntensity = 10f;
        private const float MaxFlashIntensity = 15f;
        private const float MinDoubleFlashDelay = 0.1f;
        private const float MaxDoubleFlashDelay = 0.3f;
        private const float DoubleFlashChance = 0.5f;
        private const float GlitchIntervalGrowth = 0.01f;

        [SerializeField] private Light2D lightningLight;
        [SerializeField] private float minTimeBetweenFlashes = 10f;
        [SerializeField] private float maxTimeBetweenFlashes = 30f;
        [SerializeField] private float flashDuration = 0.2f;
        [SerializeField] private float uiGlitchDuration = 3f;
        [SerializeField] private float glitchInterval = 0.1f;
        [SerializeField] private BombController bombController;
        [SerializeField] private GameObject lightningSound;

        void Start()
        {
            lightningLight.intensity = 0f;
            StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minTimeBetweenFlashes, maxTimeBetweenFlashes));

                yield return Flash();
                StartCoroutine(GlitchUI());
                AudioManager.Instance.PlaySound(lightningSound);

                if (Random.value > DoubleFlashChance)
                {
                    yield return new WaitForSeconds(Random.Range(MinDoubleFlashDelay, MaxDoubleFlashDelay));
                    yield return Flash();
                }
            }
        }

        private IEnumerator Flash()
        {
            lightningLight.intensity = Random.Range(MinFlashIntensity, MaxFlashIntensity);
            yield return new WaitForSeconds(flashDuration);
            lightningLight.intensity = 0f;
        }

        private IEnumerator GlitchUI()
        {
            float elapsedTime = 0f;
            float currentInterval = glitchInterval;

            while (elapsedTime < uiGlitchDuration)
            {
                bombController.ShowGlitchedDisplay();

                yield return new WaitForSeconds(currentInterval);
                elapsedTime += currentInterval;
                currentInterval += GlitchIntervalGrowth;
            }

            bombController.RestoreDisplay();
        }
    }
}
