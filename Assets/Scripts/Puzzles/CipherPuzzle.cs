using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class CipherPuzzle : PuzzleBase
{
    public LetterField[] letterFields;
    public TextMeshProUGUI encryptedText;
    public Button checkButton;
    public CipherGenerator cipherGenerator;

    private string correctWord;
    private List<char> allLetters;

    private void Start()
    {
        Initialize();
        checkButton.onClick.AddListener(CheckSolution);
    }

    public override void Initialize()
    {
        encryptedText.text = cipherGenerator.GenerateCipher(out correctWord);
        System.Random random = new System.Random();

        for (int i = 0; i < letterFields.Length; i++)
        {
            List<char> availableLetters = new List<char> { correctWord[i] };

            List<char> pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
            pool.Remove(correctWord[i]);

            while (availableLetters.Count < 9)
            {
                char extra = pool[random.Next(pool.Count)];
                availableLetters.Add(extra);
                pool.Remove(extra);
            }

            availableLetters = availableLetters.OrderBy(x => random.Next()).ToList();
            
            Debug.Log($"Pole {i + 1}: Poprawna litera: {correctWord[i]} | Pula liter: {string.Join(", ", availableLetters)}");
            
            letterFields[i].Initialize(correctWord[i], availableLetters);
        }
    }

    private void CheckSolution()
    {
        string playerWord = "";
        foreach (var field in letterFields)
        {
            playerWord += field.GetCurrentLetter();
        }
        if (playerWord == correctWord)
        {
            OnComplete();
        }
        else
        {
            OnFail();
        }
    }
}
