using UnityEngine;
using UnityEngine.EventSystems;

namespace DefuseOrLose
{
    public class ButtonPuzzlePlay : ButtonPuzzle, IPointerDownHandler, IPointerUpHandler
    {
        private const float HoldToleranceSeconds = 1f;

        private float pressStartTime;
        private float holdDuration;
        private int secondsToHold;

        protected override void Start()
        {
            base.Start();
            secondsToHold = BombController.Instance.BatteryBars;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            pressStartTime = Time.time;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            holdDuration = Time.time - pressStartTime;
        }

        protected override void ExecutePuzzle()
        {
            bool isHeldLongEnough = Mathf.Abs(holdDuration - secondsToHold) <= HoldToleranceSeconds;
            buttonPuzzleManager.SubmitAnswer(isHeldLongEnough);
        }
    }
}
