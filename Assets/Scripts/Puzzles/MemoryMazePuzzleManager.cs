using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DefuseOrLose
{
    public class MemoryMazePuzzleManager : PuzzleBase
    {
        private const int PhaseCount = 5;
        private const int ButtonCount = 4;
        private const float DelayBeforeBlinkSeconds = 1f;

        [Header("UI References")]
        [SerializeField] private Button[] buttons;

        [Header("Blinking Settings")]
        [SerializeField] private Color normalColor = new Color(0f, 0.91f, 0f);
        [SerializeField] private Color blinkColor = Color.black;
        [SerializeField] private float blinkDuration = 0.5f;

        private readonly ButtonPosition[] blinkedPositions = new ButtonPosition[PhaseCount];
        private readonly ButtonPosition[] pressedPositions = new ButtonPosition[PhaseCount];
        private int currentPhase = 0;
        private bool phaseActive = false;

        void Start()
        {
            Initialize();
        }

        public override void Initialize()
        {
            if (buttons == null || buttons.Length < ButtonCount)
            {
                Debug.LogError("MemoryMazePuzzleManager: all four buttons must be assigned.");
                return;
            }

            currentPhase = 0;
            StartCoroutine(StartPhaseCoroutine());
        }

        public void OnButtonPressed(int buttonIndex)
        {
            if (!phaseActive)
                return;

            ButtonPosition pressed = (ButtonPosition)buttonIndex;
            if (pressed != GetExpectedPosition(currentPhase))
            {
                OnFail();
                return;
            }

            pressedPositions[currentPhase] = pressed;
            phaseActive = false;
            currentPhase++;

            if (currentPhase >= PhaseCount)
                OnComplete();
            else
                StartCoroutine(StartPhaseCoroutine());
        }

        IEnumerator StartPhaseCoroutine()
        {
            phaseActive = false;
            yield return new WaitForSeconds(DelayBeforeBlinkSeconds);

            ButtonPosition chosen = (ButtonPosition)Random.Range(0, ButtonCount);
            blinkedPositions[currentPhase] = chosen;
            EditorLog.Log($"Memory phase {currentPhase + 1}: blink {chosen}, press {GetExpectedPosition(currentPhase)}");

            yield return StartCoroutine(BlinkButton(chosen));
            phaseActive = true;
        }

        IEnumerator BlinkButton(ButtonPosition position)
        {
            Button button = buttons[(int)position];
            button.interactable = false;
            button.image.color = blinkColor;
            yield return new WaitForSeconds(blinkDuration);
            button.interactable = true;
            button.image.color = normalColor;
        }

        ButtonPosition GetExpectedPosition(int phase)
        {
            switch (phase)
            {
                case 0: return GetExpectedInFirstPhase();
                case 1: return GetExpectedInSecondPhase();
                case 2: return GetExpectedInThirdPhase();
                case 3: return GetExpectedInFourthPhase();
                default: return GetExpectedInFifthPhase();
            }
        }

        ButtonPosition GetExpectedInFirstPhase()
        {
            switch (blinkedPositions[0])
            {
                case ButtonPosition.TL: return ButtonPosition.TR;
                case ButtonPosition.TR: return ButtonPosition.BL;
                case ButtonPosition.BL: return ButtonPosition.BR;
                default: return ButtonPosition.TL;
            }
        }

        ButtonPosition GetExpectedInSecondPhase()
        {
            switch (blinkedPositions[1])
            {
                case ButtonPosition.TL: return blinkedPositions[0];
                case ButtonPosition.TR: return pressedPositions[0];
                case ButtonPosition.BL: return GetOpposite(blinkedPositions[0]);
                default: return blinkedPositions[0];
            }
        }

        ButtonPosition GetExpectedInThirdPhase()
        {
            switch (blinkedPositions[2])
            {
                case ButtonPosition.TL: return BombController.Instance.Strikes >= 1 ? blinkedPositions[1] : blinkedPositions[0];
                case ButtonPosition.TR: return pressedPositions[0];
                case ButtonPosition.BL: return blinkedPositions[1];
                default: return GetOpposite(pressedPositions[0]);
            }
        }

        ButtonPosition GetExpectedInFourthPhase()
        {
            switch (blinkedPositions[3])
            {
                case ButtonPosition.TL: return pressedPositions[0];
                case ButtonPosition.TR: return pressedPositions[1];
                case ButtonPosition.BL: return blinkedPositions[2];
                default: return GetOpposite(pressedPositions[1]);
            }
        }

        ButtonPosition GetExpectedInFifthPhase()
        {
            switch (blinkedPositions[4])
            {
                case ButtonPosition.TL: return blinkedPositions[0];
                case ButtonPosition.TR: return blinkedPositions[1];
                case ButtonPosition.BL: return blinkedPositions[3];
                default: return blinkedPositions[2];
            }
        }

        static ButtonPosition GetOpposite(ButtonPosition position)
        {
            switch (position)
            {
                case ButtonPosition.TL: return ButtonPosition.BR;
                case ButtonPosition.TR: return ButtonPosition.BL;
                case ButtonPosition.BL: return ButtonPosition.TR;
                default: return ButtonPosition.TL;
            }
        }
    }
}
