using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CipherPuzzle : MonoBehaviour
{
    public LetterField[] letterFields;
    public TextMeshProUGUI encryptedText;
    public Image statusLight;
    public Button checkButton;

    private string correctWord = "CECHA";
    private List<char> extraLetters = new List<char> { 'X', 'M', 'T', 'B', 'P' };

    private void Start()
    {
        InitializePuzzle();
        checkButton.onClick.AddListener(CheckSolution);
    }

    private void InitializePuzzle()
    {
        encryptedText.text = "???";

        for (int i = 0; i < correctWord.Length; i++)
        {
            letterFields[i].Initialize(correctWord[i], extraLetters);
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
        }
        else
        {
            statusLight.color = Color.red;
        }
    }
}
