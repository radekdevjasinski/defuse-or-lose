using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ButtonSequencePuzzle : PuzzleBase
{
    public Image[] instructionImages;
    public Button[] buttons;
    public Sprite[] letterSprites;
    public Sprite[] symbolSprites;

    private Dictionary<string, Sprite> letterDictionary;
    private Dictionary<string, Sprite> symbolDictionary;
    private Dictionary<Button, (string position, string symbol)> buttonData;

    private List<Button> correctOrder;
    private List<string> correctHints;
    private List<bool> isHintPosition;
    private int currentStep;

    public void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        SetupDictionaries();
        AssignSymbolsToButtons();
        GenerateNewPuzzle();
    }

    private void SetupDictionaries()
    {
        letterDictionary = letterSprites.ToDictionary(sprite => sprite.name.ToLower(), sprite => sprite);
        symbolDictionary = symbolSprites.ToDictionary(sprite => sprite.name.ToLower(), sprite => sprite);
    }

    private void AssignSymbolsToButtons()
    {
        buttonData = new Dictionary<Button, (string position, string symbol)>();

        List<string> symbolKeys = symbolDictionary.Keys.ToList();
        symbolKeys = symbolKeys.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.sprite = symbolDictionary[symbolKeys[i]];
        }

        Button leftButton = null, rightButton = null, topButton = null, downButton = null;
        float minX = float.MaxValue, maxX = float.MinValue, minY = float.MaxValue, maxY = float.MinValue;

        foreach (Button b in buttons)
        {
            RectTransform rt = b.GetComponent<RectTransform>();
            Vector2 pos = rt.anchoredPosition;
            if (pos.x < minX) { minX = pos.x; leftButton = b; }
            if (pos.x > maxX) { maxX = pos.x; rightButton = b; }
            if (pos.y < minY) { minY = pos.y; downButton = b; }
            if (pos.y > maxY) { maxY = pos.y; topButton = b; }
        }

        foreach (Button b in buttons)
        {
            string posLabel = "";
            if (b == leftButton) posLabel = "left";
            else if (b == rightButton) posLabel = "right";
            else if (b == topButton) posLabel = "top";
            else if (b == downButton) posLabel = "down";
            else posLabel = "unknown";

            string symName = b.image.sprite.name.ToLower();
            buttonData[b] = (posLabel, symName);
        }

        Debug.Log("(Position -> Symbol):");
        foreach (var entry in buttonData)
        {
            Debug.Log($"{entry.Value.position.ToUpper()} -> {entry.Value.symbol.ToUpper()}");
        }
    }

    private void GenerateNewPuzzle()
    {
        Debug.Log("Generate new Sequence");
        foreach (var button in buttons)
            button.interactable = true;

        List<Button> shuffledButtons = buttonData.Keys.ToList();
        shuffledButtons = shuffledButtons.OrderBy(x => Random.value).ToList();

        correctOrder = new List<Button>();
        correctHints = new List<string>();
        isHintPosition = new List<bool>();

        foreach (Button b in shuffledButtons)
        {
            correctOrder.Add(b);
            bool usePos = (Random.value < 0.5f);
            isHintPosition.Add(usePos);
            if (usePos)
                correctHints.Add(buttonData[b].position);
            else
                correctHints.Add(buttonData[b].symbol);
        }

        Debug.Log($"Correct order: {string.Join(", ", correctHints)}");
        currentStep = 0;
        UpdateInstructionDisplay();

        foreach (var button in buttons)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    private void UpdateInstructionDisplay()
    {
        if (currentStep < correctOrder.Count)
        {
            string nextHint = correctHints[currentStep].ToUpper();
            Debug.Log($"Click: {nextHint}");
            for (int i = 0; i < instructionImages.Length; i++)
            {
                if (i < nextHint.Length)
                {
                    instructionImages[i].gameObject.SetActive(true);
                    char letter = nextHint[i];
                    string letterKey = letter.ToString().ToLower();
                    if (letterDictionary.ContainsKey(letterKey))
                    {
                        instructionImages[i].sprite = letterDictionary[letterKey];
                    }
                }
                else
                {
                    instructionImages[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void OnButtonClick(Button clickedButton)
    {
        if (currentStep >= correctOrder.Count)
            return;

        var clickedData = buttonData[clickedButton];
        string clickedSym = clickedData.symbol;
        string clickedPos = clickedData.position;

        Debug.Log($"Clicked: {clickedSym.ToUpper()} ({clickedPos.ToUpper()})");

        string expected = correctHints[currentStep];
        bool expectedIsPos = isHintPosition[currentStep];

        bool isCorrect = expectedIsPos ? (clickedPos == expected) : (clickedSym == expected);

        if (isCorrect)
        {
            Debug.Log($"Correct: {expected}");
            clickedButton.interactable = false;
            currentStep++;

            if (currentStep >= correctOrder.Count)
                OnComplete();
            else
                UpdateInstructionDisplay();
        }
        else
        {
            OnFail();
        }
    }
}
