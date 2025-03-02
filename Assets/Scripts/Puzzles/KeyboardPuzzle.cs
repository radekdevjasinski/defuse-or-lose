using UnityEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class KeyboardPuzzle : PuzzleBase
{
    [Header("Refs")]
    public BombController bombController;
    private string userName = "unknown user";
    private int day;
    private int month;
    private int year;

    private string puzzleAnswer;

    [Header("Keyboard")]
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private Transform keysParent;
    private static readonly int keysCount = 20;
    private List<GameObject> keys = new List<GameObject>();

    [Header("Input")]
    public string input = "";
    public TMPro.TMP_Text inputText;
    public override void Initialize()
    {
        try
        {
            userName = Environment.UserName.ToLower();
        }
        catch
        {
            userName = "unknown user";
        }
        day = DateTime.Now.Day;
        month = DateTime.Now.Month;
        year = DateTime.Now.Year;
    }

    void Start()
    {
        Initialize();
        bombController = GameObject.Find("Bomb").GetComponent<BombController>();
        CreateKeyboard();
        WriteInput();

        GenerateAnswer();
        PutAnswerInKeyboard();
        UnityEngine.Debug.Log(puzzleAnswer);

    }
    GameObject CreateKey(char keyName)
    {
        GameObject key = Instantiate(keyPrefab, keysParent);
        key.name = keyName.ToString() + " key";
        key.GetComponent<Key>().SetKey(keyName, this);
        key.GetComponentInChildren<TMPro.TMP_Text>().text = keyName.ToString();
        return key;
    }
    void CreateKeyboard()
    {
        const string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (int i = 0; i < keysCount; i++)
        {
            keys.Add(CreateKey(characters[UnityEngine.Random.Range(0, characters.Length)]));
        }
    }
    public void WriteInput()
    {
        inputText.text = input;
        for (int i=0; i < 5 - input.Length; i++)
        {
            inputText.text += "_";
        }
    }
    public void KeyClicked(char key)
    {
        if (input.Length < 5)
        {
            input += key;
            WriteInput();
        }
    }
    public void DeleteKey()
    {
        if (input.Length > 0)
        {
            input = input.Substring(0, input.Length - 1);
            WriteInput();
        }
    }
    public void CheckAnswer()
    {
        if (string.Equals(input, puzzleAnswer, StringComparison.OrdinalIgnoreCase))
        {
            OnComplete();
        }
        else
        {
            OnFail();
        }
    }
    void GenerateAnswer()
    {
        char answer1 = userName[0];
        string answer2 = month.ToString();
        string answer3 = day.ToString();

        switch(bombController.strikes)
        {
            case 0:
                puzzleAnswer = answer1 + answer2 + answer3; 
                break;
            case 1:
                puzzleAnswer = answer3 + answer2 + answer1; 
                break;
            case 2:
                puzzleAnswer = answer1.ToString() + answer1.ToString() + answer1.ToString();
                break;
            default:
                break;
        }
        
    }
    void PutAnswerInKeyboard()
    {
        for (int i = 0; i < puzzleAnswer.Length; i++)
        {
            do
            {
                int index = UnityEngine.Random.Range(0, keys.Count);
                if (!keys[index].GetComponent<Key>().isAnswerKey)
                {
                    keys[index].GetComponent<Key>().keyChar = puzzleAnswer[i];
                    keys[index].GetComponent<Key>().isAnswerKey = true;
                    keys[index].GetComponentInChildren<TMPro.TMP_Text>().text = puzzleAnswer[i].ToString();
                    break;
                }
            } while (true);
        }
    }
}
