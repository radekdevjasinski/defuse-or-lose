using UnityEngine;
using System.Collections;

namespace DefuseOrLose
{
    public class EnvironmentTrain : EnvironmentBase
    {
        private const float MinTunnelIntervalOffset = -1f;
        private const float MaxTunnelIntervalOffset = 5f;

        [SerializeField] private float minIntensity;
        [SerializeField] private float maxIntensity;
        [SerializeField] private float transitionDuration;
        [SerializeField] private float tunnelInterval;
        [SerializeField] private float tunnelDuration;
        [SerializeField] private float cameraShakeAmount;
        [SerializeField] private float cameraShakeSpeed;

        private CameraController cameraController;

        void Start()
        {
            cameraController = FindAnyObjectByType<CameraController>();
            if (cameraController == null)
            {
                Debug.LogError("EnvironmentTrain: no CameraController in scene, camera shake is disabled.");
            }
            StartCoroutine(TunnelEffectRoutine());
        }

        void Update()
        {
            if (cameraController == null)
                return;

            float noiseTime = Time.time * cameraShakeSpeed;
            float shakeOffsetX = Mathf.PerlinNoise(noiseTime, 0f) * cameraShakeAmount - (cameraShakeAmount / 2f);
            float shakeOffsetY = Mathf.PerlinNoise(0f, noiseTime) * cameraShakeAmount - (cameraShakeAmount / 2f);
            cameraController.ShakeOffset = new Vector3(shakeOffsetX, shakeOffsetY, 0f);
        }

        private IEnumerator TunnelEffectRoutine()
        {
            while (true)
            {
                yield return WaitForRandomizedInterval(tunnelInterval, MinTunnelIntervalOffset, MaxTunnelIntervalOffset);
                yield return StartCoroutine(ChangeLightIntensity(minIntensity));
                yield return new WaitForSeconds(tunnelDuration);
                yield return StartCoroutine(ChangeLightIntensity(maxIntensity));
            }
        }

        private IEnumerator ChangeLightIntensity(float targetIntensity)
        {
            float startIntensity = globalLight.intensity;
            float elapsedTime = 0f;

            while (elapsedTime < transitionDuration)
            {
                elapsedTime += Time.deltaTime;
                globalLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / transitionDuration);
                yield return null;
            }

            globalLight.intensity = targetIntensity;
        }
    }
}
