using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LetterField : MonoBehaviour
{
    public TextMeshProUGUI letterText;
    private List<char> availableLetters;
    private int currentIndex = 0;
    public char correctLetter { get; private set; }

    public void Initialize(char correctLetter, List<char> pool)
    {
        this.correctLetter = correctLetter;
        availableLetters = new List<char>(pool);
        
        System.Random random = new System.Random();
        currentIndex = random.Next(availableLetters.Count);
        letterText.text = availableLetters[currentIndex].ToString();

    }

    public void ChangeLetter(int direction)
    {
        char previousLetter = availableLetters[currentIndex];
        currentIndex = (currentIndex + direction + availableLetters.Count) % availableLetters.Count;
        char newLetter = availableLetters[currentIndex];
        letterText.text = newLetter.ToString();
    }

    public char GetCurrentLetter()
    {
        return availableLetters[currentIndex];
    }
}
