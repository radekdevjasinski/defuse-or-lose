using UnityEngine;
using System;
using System.Collections.Generic;

public class KeyboardPuzzle : PuzzleBase
{
    [Header("Refs")]
    public BombController bombController;


    private string puzzleAnswer;

    [Header("Keyboard")]
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private Transform keysParent;
    private static readonly int keysCount = 30;
    private List<GameObject> keys = new List<GameObject>();

    [Header("Input")]
    public string input = "";
    public TMPro.TMP_Text inputText;
    public int inputLength = 10;
    public override void Initialize()
    {


    }

    void Start()
    {
        Initialize();
        bombController = GameObject.Find("Bomb").GetComponent<BombController>();
        CreateKeyboard();
        WriteInput();

        GenerateAnswer();
        PutAnswerInKeyboard();
        Debug.Log(puzzleAnswer);

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
        for (int i=0; i < inputLength - input.Length; i++)
        {
            inputText.text += "_";
        }
    }
    public void KeyClicked(char key)
    {
        if (input.Length < inputLength)
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
        string safeWord ="";
        for(int i = 0; i < inputLength; i++)
        {
            safeWord += "x";
        }
        if (string.Equals(input, puzzleAnswer, StringComparison.OrdinalIgnoreCase)
            || string.Equals(input, safeWord, StringComparison.OrdinalIgnoreCase))
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
        string userName = Environment.UserName;
        if(userName.Length > 6)
        {
            userName = userName.Substring(0, 6);
        }
        string machineName = Environment.MachineName;
        string osVersion = Environment.OSVersion.ToString().ToLower();
        int uptimeSeconds = Environment.TickCount / 1000;
        string date = DateTime.Now.ToString("ddMM");
        int memory = SystemInfo.systemMemorySize;
        string gpu = SystemInfo.graphicsDeviceName.ToLower();

        switch (bombController.strikes)
        {
            case 0:
                puzzleAnswer = userName + date;
                break;
            case 1:
                puzzleAnswer = machineName[0].ToString() + machineName[machineName.Length-1].ToString() 
                + (osVersion.Contains("windows") ? "windows" : "linux"); 
                break;
            case 2:
                puzzleAnswer = (uptimeSeconds >= 3600 ? "yes" : "no") + (memory >= 8000 ? "yes" : "no") + (gpu.Contains("nvidia") ? "yes" : "no"); 
                break;
            default:
                break;
        }

        Debug.Log($"Puzzle Answer: {puzzleAnswer}");
    }
    void PutAnswerInKeyboard()
    {
        string answerCharacters = puzzleAnswer + "x";
        for (int i = 0; i < answerCharacters.Length; i++)
        {
            do
            {
                int index = UnityEngine.Random.Range(0, keys.Count);
                if (!keys[index].GetComponent<Key>().isAnswerKey)
                {
                    keys[index].GetComponent<Key>().keyChar = answerCharacters[i];
                    keys[index].GetComponent<Key>().isAnswerKey = true;
                    keys[index].GetComponentInChildren<TMPro.TMP_Text>().text = answerCharacters[i].ToString();
                    break;
                }
            } while (true);
        }
    }
}
