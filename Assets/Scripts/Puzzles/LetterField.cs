using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace DefuseOrLose
{
    public class LetterField : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI letterText;

        private List<char> availableLetters;
        private int currentIndex = 0;

        public void Initialize(List<char> letterPool)
        {
            availableLetters = new List<char>(letterPool);
            currentIndex = Random.Range(0, availableLetters.Count);
            ShowCurrentLetter();
        }

        public void ChangeLetter(int direction)
        {
            currentIndex = (currentIndex + direction + availableLetters.Count) % availableLetters.Count;
            ShowCurrentLetter();
        }

        public char GetCurrentLetter()
        {
            return availableLetters[currentIndex];
        }

        private void ShowCurrentLetter()
        {
            letterText.text = availableLetters[currentIndex].ToString();
        }
    }
}
