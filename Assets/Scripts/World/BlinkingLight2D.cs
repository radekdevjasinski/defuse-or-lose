using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace DefuseOrLose
{
    public class BlinkingLight2D : MonoBehaviour
    {
        private const float TargetReachedTolerance = 0.05f;

        [SerializeField] private Light2D light2D;
        [SerializeField] private float minIntensity = 0.2f;
        [SerializeField] private float maxIntensity = 1.0f;
        [SerializeField] private float speed = 2.0f;
        [SerializeField] private bool randomBlinking = false;

        private float targetIntensity;
        private float t = 0;

        private void Start()
        {
            if (light2D == null)
            {
                light2D = GetComponent<Light2D>();
            }

            targetIntensity = maxIntensity;
        }

        private void Update()
        {
            if (randomBlinking)
            {
                if (Mathf.Abs(light2D.intensity - targetIntensity) < TargetReachedTolerance)
                {
                    targetIntensity = Random.Range(minIntensity, maxIntensity);
                    t = 0;
                }
            }
            else
            {
                if (light2D.intensity >= maxIntensity) targetIntensity = minIntensity;
                else if (light2D.intensity <= minIntensity) targetIntensity = maxIntensity;
            }

            t += Time.deltaTime * speed;
            light2D.intensity = Mathf.Lerp(light2D.intensity, targetIntensity, t);
        }
    }
}
