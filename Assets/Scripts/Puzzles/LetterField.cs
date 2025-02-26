using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LetterField : MonoBehaviour
{
    public TextMeshProUGUI letterText;
    private List<char> availableLetters;
    private int currentIndex = 0;

    public void Initialize(char correctLetter, List<char> extraLetters)
    {
        availableLetters = new List<char>(extraLetters);
        availableLetters.Add(correctLetter);
        currentIndex = availableLetters.IndexOf(correctLetter);
        letterText.text = availableLetters[currentIndex].ToString();
    }

    public void ChangeLetter(int direction)
    {
        currentIndex = (currentIndex + direction + availableLetters.Count) % availableLetters.Count;
        letterText.text = availableLetters[currentIndex].ToString();
    }

    public char GetCurrentLetter()
    {
        return availableLetters[currentIndex];
    }
}
