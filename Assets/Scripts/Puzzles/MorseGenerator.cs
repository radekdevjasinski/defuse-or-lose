using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefuseOrLose
{
    public class MorseGenerator : MonoBehaviour
    {
        private const int MinNumber = 1;
        private const int MaxNumber = 99;
        private const bool Dot = true;
        private const bool Dash = false;

        private static readonly Dictionary<char, bool[]> morseDigits = new Dictionary<char, bool[]>
        {
            { '0', new bool[] { Dash, Dash, Dash, Dash, Dash } },
            { '1', new bool[] { Dot, Dash, Dash, Dash, Dash } },
            { '2', new bool[] { Dot, Dot, Dash, Dash, Dash } },
            { '3', new bool[] { Dot, Dot, Dot, Dash, Dash } },
            { '4', new bool[] { Dot, Dot, Dot, Dot, Dash } },
            { '5', new bool[] { Dot, Dot, Dot, Dot, Dot } },
            { '6', new bool[] { Dash, Dot, Dot, Dot, Dot } },
            { '7', new bool[] { Dash, Dash, Dot, Dot, Dot } },
            { '8', new bool[] { Dash, Dash, Dash, Dot, Dot } },
            { '9', new bool[] { Dash, Dash, Dash, Dash, Dot } }
        };

        [FormerlySerializedAs("light_on")]
        [SerializeField] private GameObject lightOn;
        [FormerlySerializedAs("light_off")]
        [SerializeField] private GameObject lightOff;
        [SerializeField] private float dotDuration;
        [SerializeField] private float dashDuration;
        [SerializeField] private float pauseDuration;
        [SerializeField] private float letterPause;

        private Object loopOwner;

        public void StartMorseLoop(int number, Object owner)
        {
            if (number < MinNumber || number > MaxNumber)
            {
                Debug.LogError($"MorseGenerator: number must be between {MinNumber} and {MaxNumber}, got {number}.");
                return;
            }
            loopOwner = owner;
            StopAllCoroutines();
            StartCoroutine(PlayMorseLoop(number));
        }

        public void StopMorseLoop(Object owner)
        {
            if (!ReferenceEquals(loopOwner, owner))
                return;

            loopOwner = null;
            StopAllCoroutines();
            TurnLight(true);
        }

        void TurnLight(bool on)
        {
            if (lightOn != null)
            {
                lightOn.SetActive(on);
            }
            if (lightOff != null)
            {
                lightOff.SetActive(!on);
            }
        }

        private IEnumerator PlayMorseLoop(int number)
        {
            List<bool> morseCode = new List<bool>();
            foreach (char digit in number.ToString())
            {
                morseCode.AddRange(morseDigits[digit]);
            }

            TurnLight(false);
            yield return new WaitForSeconds(letterPause);
            while (true)
            {
                foreach (bool signal in morseCode)
                {
                    TurnLight(true);
                    yield return new WaitForSeconds(signal == Dot ? dotDuration : dashDuration);

                    TurnLight(false);
                    yield return new WaitForSeconds(pauseDuration);
                }

                yield return new WaitForSeconds(letterPause);
            }
        }
    }
}
