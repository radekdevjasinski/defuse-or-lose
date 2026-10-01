using System.Collections;
using System.Linq;
using UnityEngine;

namespace DefuseOrLose
{
    public class ButtonPuzzleStop : ButtonPuzzle
    {
        private const float SecondsToWaitAfterLastClick = 2f;

        private int requiredClicks;
        private int clicks = 0;

        protected override void Start()
        {
            base.Start();
            int digitSum = BombController.Instance.SerialCode.Where(char.IsDigit).Sum(digit => digit - '0');
            requiredClicks = Mathf.Max(1, digitSum);
        }

        protected override void ExecutePuzzle()
        {
            clicks++;
            StopAllCoroutines();
            StartCoroutine(SubmitAfterLastClick());
        }

        IEnumerator SubmitAfterLastClick()
        {
            yield return new WaitForSeconds(SecondsToWaitAfterLastClick);
            buttonPuzzleManager.SubmitAnswer(clicks == requiredClicks);
        }
    }
}
