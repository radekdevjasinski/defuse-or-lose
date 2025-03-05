using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal; 

public class EnvironmentStorm : EnvironmentBase
{
    public Light2D lightningLight; 
    public float minTimeBetweenFlashes = 10f;
    public float maxTimeBetweenFlashes = 30f;
    public float flashDuration = 0.2f;
    public float uiGlitchDuration = 3f;
    public float glitchInterval = 0.1f;

    public BombController bombController;
    [SerializeField] private GameObject lightningSound;

    protected override void Start()
    {
        base.Start();
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

            if (Random.value > 0.5f)
            {
                yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
                yield return Flash();
            }
        }
    }

    private IEnumerator Flash()
    {
        lightningLight.intensity = Random.Range(10f, 15f); 
        yield return new WaitForSeconds(flashDuration);
        lightningLight.intensity = 0f;
    }
    private IEnumerator GlitchUI()
    {
        float elapsedTime = 0f;
        glitchInterval = 0.1f;

        while (elapsedTime < uiGlitchDuration)
        {
            bombController.batteryImage.sprite = bombController.batterySprites[Random.Range(0, bombController.batterySprites.Count)];

            foreach (Transform child in bombController.strikeParent)
            {
                Destroy(child.gameObject);
            }
            for (int i = 0; i < Random.Range(0,5); i++)
            {
                Instantiate(bombController.strikeUI, bombController.strikeParent);
            }

            bombController.timerVisible = false;
            bombController.timerText.text = Random.Range(10, 60).ToString() + ":" + Random.Range(10, 60).ToString();

            yield return new WaitForSeconds(glitchInterval);
            elapsedTime += glitchInterval;
            glitchInterval += 0.01f;
        }

        bombController.batteryImage.sprite = bombController.batterySprites[bombController.batteryBars-1];
        bombController.WriteStrikes();
        bombController.timerVisible = true;
    }
}
