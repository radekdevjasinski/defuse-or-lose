using System;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleMorse : PuzzleBase
{
    public int code = 1;
    private MorseGenerator morseGenerator;
    [SerializeField] private Slider slider;
    public override void Initialize()
    {
        code = UnityEngine.Random.Range(1,100);
        morseGenerator = GameObject.Find("tablet_morse_light").GetComponent<MorseGenerator>();
        morseGenerator.StartMorseLoop(code);
    }
    void Start()
    {
        Initialize();
    }
    public void Submit()
    {
        int value = Mathf.FloorToInt(slider.value);
        if(value == code)
        {
            OnComplete();
        }
        else
        {
            OnFail();
        }
    }
}
