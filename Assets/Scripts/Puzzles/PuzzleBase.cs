using UnityEngine;

namespace DefuseOrLose
{
    public abstract class PuzzleBase : MonoBehaviour
    {
        public abstract void Initialize();

        protected virtual void OnComplete()
        {
            GameController.Instance.WinPuzzle();
        }

        protected virtual void OnFail()
        {
            GameController.Instance.LosePuzzle();
        }
    }
}
