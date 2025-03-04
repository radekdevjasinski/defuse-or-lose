using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public GameObject uiClickSource; 
    private AudioSource click;

    public GameObject[] events;

    private Dictionary<GameObject, AudioSource> sounds = new Dictionary<GameObject, AudioSource>();
    public GameObject[] ambients;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach (GameObject ambient in ambients)
        {
            GameObject ambientSound = Instantiate(ambient, transform);
            ambientSound.GetComponent<AudioSource>().Play();
        }
        click = Instantiate(uiClickSource,transform).GetComponent<AudioSource>();
        foreach (GameObject sound in events)
        {
            GameObject soundSource = Instantiate(sound, transform);
            sounds.Add(sound, soundSource.GetComponent<AudioSource>());
        }
    }
    void Update()
    {
         if (Input.GetMouseButtonDown(0))
        {
            PlayClickSound();
        }
    }

    public void PlayClickSound()
    {
        click.pitch = Random.Range(0.5f, 1.5f);
        click.Play();
    }
    public void PlaySound(GameObject sound)
    {
        if (sounds.ContainsKey(sound))
            sounds[sound].Play();
    }
    public void StopAllSounds()
    {
        foreach (Transform child in transform)
        {
            AudioSource audioSource = child.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }
    }

}
