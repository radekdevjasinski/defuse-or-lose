using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class EnvironmentBase : MonoBehaviour
{
    protected Camera cameraRef;
    public Light2D globalLight;
    protected virtual void Start()
    {
        cameraRef = Camera.main;
    }
}

public class EnvironmentTrain : EnvironmentBase
{
    [SerializeField] private float minIntensity; 
    [SerializeField] private float maxIntensity; 
    [SerializeField] private float transitionDuration;
    [SerializeField] private float tunnelInterval; 
    [SerializeField] private float tunnelDuration;
    [SerializeField] private float cameraShakeAmount;
    [SerializeField] private float cameraShakeSpeed;
    
    private Transform cameraTransform; 
    private Vector3 initialCameraPosition;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(TunnelEffectRoutine());
        cameraTransform = cameraRef.transform;
        initialCameraPosition = cameraTransform.localPosition;
        StartCoroutine(CameraShakeRoutine());

    }
    
    private IEnumerator TunnelEffectRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(tunnelInterval + Random.Range(-1f, 5f));
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
    private IEnumerator CameraShakeRoutine()
    {
        while (true)
        {
            float shakeOffsetX = Mathf.PerlinNoise(Time.time * cameraShakeSpeed, 0f) * cameraShakeAmount - (cameraShakeAmount / 2f);
            float shakeOffsetY = Mathf.PerlinNoise(0f, Time.time * cameraShakeSpeed) * cameraShakeAmount - (cameraShakeAmount / 2f);
            cameraTransform.localPosition = initialCameraPosition + new Vector3(shakeOffsetX, shakeOffsetY, 0);
            yield return null;
        }
    }
}
