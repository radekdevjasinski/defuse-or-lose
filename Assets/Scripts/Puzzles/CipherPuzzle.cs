using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

namespace DefuseOrLose
{
    public class CipherPuzzle : PuzzleBase
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int LettersPerField = 9;

        [SerializeField] private LetterField[] letterFields;
        [SerializeField] private TextMeshProUGUI encryptedText;
        [SerializeField] private Button checkButton;

        private readonly CipherGenerator cipherGenerator = new CipherGenerator();
        private string correctWord;

        private void Start()
        {
            Initialize();
            checkButton.onClick.AddListener(CheckSolution);
        }

        public override void Initialize()
        {
            encryptedText.text = cipherGenerator.GenerateCipher(out correctWord);
            EditorLog.Log($"Cipher: {encryptedText.text} -> {correctWord}");

            for (int i = 0; i < letterFields.Length; i++)
            {
                letterFields[i].Initialize(BuildLetterPool(correctWord[i]));
            }
        }

        private static List<char> BuildLetterPool(char correctLetter)
        {
            List<char> decoys = Alphabet.Where(letter => letter != correctLetter).ToList();
            decoys.Shuffle();

            List<char> pool = decoys.Take(LettersPerField - 1).ToList();
            pool.Add(correctLetter);
            pool.Shuffle();
            return pool;
        }

        private void CheckSolution()
        {
            string playerWord = string.Concat(letterFields.Select(field => field.GetCurrentLetter()));
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
}
