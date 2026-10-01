using System.Collections;
using UnityEngine;

namespace DefuseOrLose
{
    public class ButtonPuzzleHeart : ButtonPuzzle
    {
        private const float SecondsWithoutPressToWin = 15f;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(WinWhenLeftAlone());
        }

        protected override void ExecutePuzzle()
        {
            buttonPuzzleManager.SubmitAnswer(false);
        }

        private IEnumerator WinWhenLeftAlone()
        {
            yield return new WaitForSeconds(SecondsWithoutPressToWin);
            buttonPuzzleManager.SubmitAnswer(true);
        }
    }
}
