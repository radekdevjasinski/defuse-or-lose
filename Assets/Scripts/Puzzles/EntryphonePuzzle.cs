using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefuseOrLose
{
    public class EntryphonePuzzle : PuzzleBase
    {
        private const int MaxInputLength = 3;
        private const int SelectedSoundCount = 3;
        private const float PauseBetweenSoundsSeconds = 0.5f;
        private const char EmptyInputCharacter = '_';

        [SerializeField] private AudioClip[] clips;
        [SerializeField] private Button phoneButton;
        [SerializeField] private TMPro.TMP_Text inputText;

        private List<EntryphoneSound> sounds;
        private List<EntryphoneSound> selectedSounds;
        private string input = "";
        private int answer = 0;

        void Start()
        {
            Initialize();
        }

        public override void Initialize()
        {
            sounds = new List<EntryphoneSound>
            {
                new EntryphoneSound(clips[0], 8, new ArithmeticOperation('+', 4), new ArithmeticOperation('*', 2)),
                new EntryphoneSound(clips[1], 12, new ArithmeticOperation('-', 6), new ArithmeticOperation('+', 5)),
                new EntryphoneSound(clips[2], 15, new ArithmeticOperation('*', 2), new ArithmeticOperation('/', 5)),
                new EntryphoneSound(clips[3], 20, new ArithmeticOperation('-', 4), new ArithmeticOperation('*', 3)),
                new EntryphoneSound(clips[4], 6, new ArithmeticOperation('+', 3), new ArithmeticOperation('/', 2)),
                new EntryphoneSound(clips[5], 10, new ArithmeticOperation('*', 5), new ArithmeticOperation('-', 7)),
                new EntryphoneSound(clips[6], 30, new ArithmeticOperation('/', 3), new ArithmeticOperation('+', 15))
            };

            SelectSounds();
            answer = CalculateAnswer();
            WriteInput();
            EditorLog.Log($"Entryphone answer: {answer}");
        }

        public void PhoneButton()
        {
            StopAllCoroutines();
            StartCoroutine(PlaySequence());
            phoneButton.interactable = false;
        }

        public void CheckAnswer()
        {
            if (int.TryParse(input, out int inputAnswer) && inputAnswer == answer)
            {
                OnComplete();
            }
            else
            {
                OnFail();
            }
        }

        public void Key(string key)
        {
            if (input.Length < MaxInputLength)
            {
                input += key;
            }
            WriteInput();
        }

        public void ClearInput()
        {
            input = "";
            WriteInput();
        }

        void SelectSounds()
        {
            List<EntryphoneSound> shuffledSounds = new List<EntryphoneSound>(sounds);
            shuffledSounds.Shuffle();
            selectedSounds = shuffledSounds.GetRange(0, SelectedSoundCount);
        }

        IEnumerator PlaySequence()
        {
            foreach (EntryphoneSound sound in selectedSounds)
            {
                AudioManager.Instance.PlayClip(sound.Clip);
                yield return new WaitForSeconds(sound.Clip.length + PauseBetweenSoundsSeconds);
            }
            phoneButton.interactable = true;
        }

        int CalculateAnswer()
        {
            float value = selectedSounds[0].StartingNumber;
            value = selectedSounds[1].FirstOperation.Apply(value);
            value = selectedSounds[2].SecondOperation.Apply(value);
            return Mathf.Abs(Mathf.FloorToInt(value));
        }

        void WriteInput()
        {
            inputText.text = input.PadLeft(MaxInputLength, EmptyInputCharacter);
        }
    }
}
