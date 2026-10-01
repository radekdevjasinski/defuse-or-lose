using UnityEngine;
using UnityEngine.UI;

namespace DefuseOrLose
{
    public class PuzzleMorse : PuzzleBase
    {
        private const int MinCode = 1;
        private const int MaxCode = 99;

        [SerializeField] private Slider slider;

        private MorseGenerator morseGenerator;
        private int code;

        void Start()
        {
            Initialize();
        }

        void OnDestroy()
        {
            if (morseGenerator != null)
            {
                morseGenerator.StopMorseLoop(this);
            }
        }

        public override void Initialize()
        {
            code = Random.Range(MinCode, MaxCode + 1);
            EditorLog.Log($"Morse answer: {code}");

            morseGenerator = FindAnyObjectByType<MorseGenerator>();
            if (morseGenerator == null)
            {
                Debug.LogError("PuzzleMorse: no MorseGenerator in scene.");
                return;
            }
            morseGenerator.StartMorseLoop(code, this);
        }

        public void Submit()
        {
            if (Mathf.FloorToInt(slider.value) == code)
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
