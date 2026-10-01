using UnityEngine;
using System;
using System.Collections.Generic;

namespace DefuseOrLose
{
    public class KeyboardPuzzle : PuzzleBase
    {
        private const string KeyCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int KeysCount = 30;
        private const char SafeWordCharacter = 'x';
        private const char EmptyInputCharacter = '_';
        private const int MaxUserNameLength = 6;
        private const int MaxStrikesWithOwnAnswer = 2;
        private const int MillisecondsPerSecond = 1000;
        private const int LongUptimeSeconds = 3600;
        private const float MegabytesPerGigabyte = 1024f;
        private const int LargeMemoryGigabytes = 8;

        [Header("Keyboard")]
        [SerializeField] private GameObject keyPrefab;
        [SerializeField] private Transform keysParent;

        [Header("Input")]
        [SerializeField] private TMPro.TMP_Text inputText;
        [SerializeField] private int inputLength = 10;

        private readonly List<Key> keys = new List<Key>();
        private string puzzleAnswer;
        private string input = "";

        void Start()
        {
            Initialize();
        }

        public override void Initialize()
        {
            CreateKeyboard();
            WriteInput();

            puzzleAnswer = GenerateAnswer();
            PutAnswerInKeyboard();
            EditorLog.Log($"Keyboard answer: {puzzleAnswer}");
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
            string safeWord = new string(SafeWordCharacter, inputLength);
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

        void CreateKeyboard()
        {
            for (int i = 0; i < KeysCount; i++)
            {
                Key key = Instantiate(keyPrefab, keysParent).GetComponent<Key>();
                key.SetKey(KeyCharacters[UnityEngine.Random.Range(0, KeyCharacters.Length)], this);
                keys.Add(key);
            }
        }

        void WriteInput()
        {
            inputText.text = input.PadRight(inputLength, EmptyInputCharacter);
        }

        string GenerateAnswer()
        {
            switch (Mathf.Min(BombController.Instance.Strikes, MaxStrikesWithOwnAnswer))
            {
                case 0:
                    return BuildUserAnswer();
                case 1:
                    return BuildMachineAnswer();
                default:
                    return BuildHardwareAnswer();
            }
        }

        static string BuildUserAnswer()
        {
            string userName = Environment.UserName;
            if (userName.Length > MaxUserNameLength)
            {
                userName = userName.Substring(0, MaxUserNameLength);
            }
            return userName + DateTime.Now.ToString("ddMM");
        }

        static string BuildMachineAnswer()
        {
            string machineName = Environment.MachineName;
            string osVersion = Environment.OSVersion.ToString().ToLowerInvariant();
            return machineName[0].ToString() + machineName[machineName.Length - 1]
                + (osVersion.Contains("windows") ? "windows" : "linux");
        }

        static string BuildHardwareAnswer()
        {
            long uptimeSeconds = (uint)Environment.TickCount / MillisecondsPerSecond;
            int memoryGigabytes = Mathf.RoundToInt(SystemInfo.systemMemorySize / MegabytesPerGigabyte);
            string gpu = SystemInfo.graphicsDeviceName.ToLowerInvariant();

            return ToYesNo(uptimeSeconds >= LongUptimeSeconds)
                + ToYesNo(memoryGigabytes >= LargeMemoryGigabytes)
                + ToYesNo(gpu.Contains("nvidia"));
        }

        static string ToYesNo(bool condition)
        {
            return condition ? "yes" : "no";
        }

        void PutAnswerInKeyboard()
        {
            string answerCharacters = puzzleAnswer + SafeWordCharacter;
            List<Key> shuffledKeys = new List<Key>(keys);
            shuffledKeys.Shuffle();

            for (int i = 0; i < answerCharacters.Length; i++)
            {
                shuffledKeys[i].SetCharacter(answerCharacters[i]);
            }
        }
    }
}
