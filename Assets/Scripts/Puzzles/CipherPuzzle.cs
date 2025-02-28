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

        List<char> allLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
        allLetters.Remove(correctWord[i]);

        while (availableLetters.Count < 9)
        {
            char extra = allLetters[random.Next(allLetters.Count)];
            availableLetters.Add(extra);
            allLetters.Remove(extra);
        }

        availableLetters = availableLetters.OrderBy(x => random.Next()).ToList();
         letterFields[i].Initialize(correctWord[i], availableLetters);

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
