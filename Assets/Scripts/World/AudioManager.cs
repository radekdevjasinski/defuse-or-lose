using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefuseOrLose
{
    public class AudioManager : MonoBehaviour
    {
        private const float MinClickPitch = 0.5f;
        private const float MaxClickPitch = 1.5f;

        public static AudioManager Instance { get; private set; }

        [SerializeField] private GameObject uiClickSource;
        [SerializeField] private GameObject[] events;
        [SerializeField] private GameObject[] ambients;

        private readonly Dictionary<GameObject, AudioSource> sounds = new Dictionary<GameObject, AudioSource>();
        private AudioSource click;
        private AudioSource clipSource;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            clipSource = gameObject.AddComponent<AudioSource>();
            clipSource.playOnAwake = false;
        }

        private void Start()
        {
            foreach (GameObject ambient in ambients)
            {
                GameObject ambientSound = Instantiate(ambient, transform);
                ambientSound.GetComponent<AudioSource>().Play();
            }
            click = Instantiate(uiClickSource, transform).GetComponent<AudioSource>();
            foreach (GameObject sound in events)
            {
                GameObject soundSource = Instantiate(sound, transform);
                sounds.Add(sound, soundSource.GetComponent<AudioSource>());
            }
        }

        void Update()
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                PlayClickSound();
            }
        }

        public void PlayClickSound()
        {
            click.pitch = Random.Range(MinClickPitch, MaxClickPitch);
            click.Play();
        }

        public void PlaySound(GameObject sound)
        {
            if (!sounds.TryGetValue(sound, out AudioSource source))
            {
                Debug.LogWarning($"AudioManager: sound '{sound.name}' is not registered in events.");
                return;
            }
            source.Play();
        }

        public void PlayClip(AudioClip clip)
        {
            clipSource.PlayOneShot(clip);
        }

        public void StopAllSounds()
        {
            clipSource.Stop();
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
}
