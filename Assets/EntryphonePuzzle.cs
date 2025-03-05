using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound
{
    public GameObject audioObject;
    public int startingNumber;
    public char operation1;
    public int secondNumber;
    public char operation2;
    public int thirdNumber;
    public Sound(GameObject audioObject, int startingNumber, char operation1, int secondNumber, char operation2, int thirdNumber)
    {
        this.audioObject = audioObject;
        this.startingNumber = startingNumber;
        this.operation1 = operation1;
        this.secondNumber = secondNumber;
        this.operation2 = operation2;
        this.thirdNumber = thirdNumber;
    }
    public float Operation1(float value)
    {
        switch (operation1)
        {
            case '+':
                return value + secondNumber;
            case '-':
                return value - secondNumber;
            case '*':
                return value * secondNumber;
            case '/':
                return value / secondNumber;
            default:
                return value;
        }
    }
    public float Operation2(float value)
    {
        switch (operation2)
        {
            case '+':
                return value + thirdNumber;
            case '-':
                return value - thirdNumber;
            case '*':
                return value * thirdNumber;
            case '/':
                return value / thirdNumber;
            default:
                return value;
        }
    }
}

public class EntryphonePuzzle : PuzzleBase
{
    public AudioClip[] clips;
    public List<Sound> sounds = new List<Sound>();
    public List<Sound> selectedSounds;
    public Button phoneButton;
    public string input = "";
    public TMPro.TMP_Text inputText;
    private int answer = 0;
    public override void Initialize()
    {
        sounds = new List<Sound>
        {
            new Sound(CreateAudioObject(clips[0]), 8, '+', 4, '*', 2),
            new Sound(CreateAudioObject(clips[1]), 12, '-', 6, '+', 5),
            new Sound(CreateAudioObject(clips[2]), 15, '*', 2, '/', 5),
            new Sound(CreateAudioObject(clips[3]), 20, '-', 4, '*', 3),
            new Sound(CreateAudioObject(clips[4]), 6, '+', 3, '/', 2),
            new Sound(CreateAudioObject(clips[5]), 10, '*', 5, '-', 7),
            new Sound(CreateAudioObject(clips[6]), 30, '/', 3, '+', 15)
        };

    }
    void Start()
    {
        Initialize();
        SelectSounds();
        WriteInput();
        CalculateAnswer();
        Debug.Log(answer);
    }

    GameObject CreateAudioObject(AudioClip clip)
    {
        GameObject audioObject = new GameObject("Audio_" + clip.name);
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.playOnAwake = false; 

        AudioManager.Instance.AddSound(audioObject);
        return audioObject;
    }
    void SelectSounds()
    {
        selectedSounds = new List<Sound>();
        List<Sound> randomSounds = new List<Sound>();
        randomSounds.AddRange(sounds);
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, randomSounds.Count);
            selectedSounds.Add(randomSounds[index]);
            randomSounds.RemoveAt(index);
        }
    }
    IEnumerator PlaySequence()
    {
        foreach (Sound sound in selectedSounds)
        {
            AudioManager.Instance.PlaySound(sound.audioObject);
            yield return new WaitForSeconds(sound.audioObject.GetComponent<AudioSource>().clip.length + .5f);
        }
        phoneButton.interactable = true;
    }
    public void PhoneButton()
    {
        StopAllCoroutines();
        StartCoroutine(PlaySequence());
        phoneButton.interactable = false;
    }
    void CalculateAnswer()
    {
        float value = selectedSounds[0].startingNumber;
        value = selectedSounds[1].Operation1(value);
        value = selectedSounds[2].Operation2(value);
        answer = Mathf.FloorToInt(value);
        answer = Mathf.Abs(answer);
     } 
    public void CheckAnswer()
    {
        if (input.Length <= 0)
        {
            OnFail();
            return;
        }
        int inputAnswer = int.Parse(input);
        CalculateAnswer();
        if (answer == inputAnswer)
        {
            OnComplete();
        }
        else
        {
            OnFail();
        }
    }
    public void Key(string key)
    {
        if (input.Length <= 3)
        {
            input += key;
        }
        WriteInput();
    }
    public void Reset()
    {
        input = "";
        WriteInput();
    }
    void WriteInput()
    {
        inputText.text = "";
        for (int i = 0; i < 3 - input.Length; i++)
        {
            inputText.text += "_";
        }
        inputText.text += input;
    }

}
