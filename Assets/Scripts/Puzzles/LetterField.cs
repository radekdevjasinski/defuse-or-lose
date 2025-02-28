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
        char previousLetter = availableLetters[currentIndex];
        currentIndex = (currentIndex + direction + availableLetters.Count) % availableLetters.Count;
        char newLetter = availableLetters[currentIndex];
        
        letterText.text = availableLetters[currentIndex].ToString();

        Debug.Log($"Zmieniono literę: {previousLetter} -> {newLetter}");
    }

    public char GetCurrentLetter()
    {
        return availableLetters[currentIndex];
    }
}
