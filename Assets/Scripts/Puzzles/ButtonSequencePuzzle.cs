using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace DefuseOrLose
{
    public class ButtonSequencePuzzle : PuzzleBase
    {
        private const float PositionHintChance = 0.5f;

        [SerializeField] private Image[] instructionImages;
        [SerializeField] private Button[] buttons;
        [SerializeField] private Sprite[] letterSprites;
        [SerializeField] private Sprite[] symbolSprites;

        private Dictionary<string, Sprite> letterDictionary;
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
            letterDictionary = letterSprites.ToDictionary(sprite => sprite.name.ToLowerInvariant(), sprite => sprite);
            AssignSymbolsToButtons();
            LabelButtons();
            GenerateNewPuzzle();
        }

        private void AssignSymbolsToButtons()
        {
            List<Sprite> shuffledSymbols = symbolSprites.ToList();
            shuffledSymbols.Shuffle();

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].image.sprite = shuffledSymbols[i];
            }
        }

        private void LabelButtons()
        {
            Button leftButton = buttons.OrderBy(button => GetAnchoredPosition(button).x).First();
            Button rightButton = buttons.OrderByDescending(button => GetAnchoredPosition(button).x).First();
            Button downButton = buttons.OrderBy(button => GetAnchoredPosition(button).y).First();
            Button topButton = buttons.OrderByDescending(button => GetAnchoredPosition(button).y).First();

            buttonData = new Dictionary<Button, (string position, string symbol)>();
            foreach (Button button in buttons)
            {
                string positionLabel;
                if (button == leftButton) positionLabel = "left";
                else if (button == rightButton) positionLabel = "right";
                else if (button == topButton) positionLabel = "top";
                else if (button == downButton) positionLabel = "down";
                else positionLabel = "unknown";

                buttonData[button] = (positionLabel, button.image.sprite.name.ToLowerInvariant());
            }
        }

        private static Vector2 GetAnchoredPosition(Button button)
        {
            return button.GetComponent<RectTransform>().anchoredPosition;
        }

        private void GenerateNewPuzzle()
        {
            correctOrder = buttonData.Keys.ToList();
            correctOrder.Shuffle();
            correctHints = new List<string>();
            isHintPosition = new List<bool>();

            foreach (Button button in correctOrder)
            {
                bool usePosition = Random.value < PositionHintChance;
                isHintPosition.Add(usePosition);
                correctHints.Add(usePosition ? buttonData[button].position : buttonData[button].symbol);
            }

            EditorLog.Log($"Correct order: {string.Join(", ", correctHints)}");
            currentStep = 0;
            UpdateInstructionDisplay();

            foreach (Button button in buttons)
            {
                button.interactable = true;
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnButtonClick(button));
            }
        }

        private void UpdateInstructionDisplay()
        {
            string nextHint = correctHints[currentStep];
            for (int i = 0; i < instructionImages.Length; i++)
            {
                bool hasLetter = i < nextHint.Length;
                instructionImages[i].gameObject.SetActive(hasLetter);
                if (hasLetter && letterDictionary.TryGetValue(nextHint[i].ToString(), out Sprite letterSprite))
                {
                    instructionImages[i].sprite = letterSprite;
                }
            }
        }

        public void OnButtonClick(Button clickedButton)
        {
            if (currentStep >= correctOrder.Count)
                return;

            (string position, string symbol) clickedData = buttonData[clickedButton];
            string clickedHint = isHintPosition[currentStep] ? clickedData.position : clickedData.symbol;

            if (clickedHint != correctHints[currentStep])
            {
                OnFail();
                return;
            }

            clickedButton.interactable = false;
            currentStep++;

            if (currentStep >= correctOrder.Count)
                OnComplete();
            else
                UpdateInstructionDisplay();
        }
    }
}
