using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseGenerator : MonoBehaviour
{
    //true kropka, false kreska
    private static readonly Dictionary<char, bool[]> morseDigits = new Dictionary<char, bool[]>
    {
        { '0', new bool[] { false, false, false, false, false } },
        { '1', new bool[] { true, false, false, false, false } },
        { '2', new bool[] { true, true, false, false, false } },
        { '3', new bool[] { true, true, true, false, false } },
        { '4', new bool[] { true, true, true, true, false } },
        { '5', new bool[] { true, true, true, true, true } },
        { '6', new bool[] { false, true, true, true, true } },
        { '7', new bool[] { false, false, true, true, true } },
        { '8', new bool[] { false, false, false, true, true } },
        { '9', new bool[] { false, false, false, false, true } }
    };

    public GameObject light_on; 
    public GameObject light_off; 
    public float dotDuration;
    public float dashDuration;
    public float pauseDuration;
    public float letterPause;

    private bool isPlaying = false;

    public void StartMorseLoop(int number)
    {
        Debug.Log(number);
        if (number < 1 || number > 99)
        {
            Debug.LogError("Liczba musi być w zakresie 1-99!");
            return;
        }
        StopMorseLoop();
        TurnLight(false);
        isPlaying = true;
        StartCoroutine(PlayMorseLoop(number));
    }

    void TurnLight(bool on)
    {

        light_on?.SetActive(on);
        light_off?.SetActive(!on);

    }

    public void StopMorseLoop()
    {
        isPlaying = false;
        StopAllCoroutines();
        TurnLight(true);
    }

    private IEnumerator PlayMorseLoop(int number)
    {
        string numberStr = number.ToString();
        List<bool> morseCode = new List<bool>();

        foreach (char digit in numberStr)
        {
            morseCode.AddRange(morseDigits[digit]);
        }
        
        TurnLight(false);
        yield return new WaitForSeconds(letterPause);
        while (isPlaying)
        {
            foreach (bool signal in morseCode)
            {
                if (!isPlaying) yield break;

                TurnLight(true);
                yield return new WaitForSeconds(signal ? dotDuration : dashDuration);

                TurnLight(false);
                yield return new WaitForSeconds(pauseDuration);
            }

            yield return new WaitForSeconds(letterPause); 
        }
    }
}
