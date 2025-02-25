using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlinkingLight2D : MonoBehaviour
{
    public Light2D light2D;  
    public float minIntensity = 0.2f; 
    public float maxIntensity = 1.0f; 
    public float speed = 2.0f;  
    public bool randomBlinking = false; 

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
            if (Mathf.Abs(light2D.intensity - targetIntensity) < 0.05f)
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
