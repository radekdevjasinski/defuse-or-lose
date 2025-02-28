using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class CipherPuzzle : PuzzleBase
{
    public LetterField[] letterFields;
    public TextMeshProUGUI encryptedText;
    public Image statusLight;
    public Button checkButton;
    public CipherGenerator cipherGenerator;

    private string correctWord;
    private string encryptedWord;
    private List<char> allLetters = new List<char>("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

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

        List<char> allLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
        allLetters.Remove(correctWord[i]);

        while (availableLetters.Count < 9)
        {
            char extra = allLetters[random.Next(allLetters.Count)];
            availableLetters.Add(extra);
            allLetters.Remove(extra);
        }

        availableLetters = availableLetters.OrderBy(x => random.Next()).ToList();

        Debug.Log($"Pole {i + 1}: {correctWord[i]} | Lista wyboru: {string.Join(", ", availableLetters)}");

        letterFields[i].Initialize(correctWord[i], availableLetters);
    }

    statusLight.color = Color.white;
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
            statusLight.color = Color.green;
            OnComplete();
        }
        else
        {
            statusLight.color = Color.red;
        }
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        Debug.Log("Cipher puzzle solved!");
    }
}
