using UnityEngine;
using UnityEngine.UI;

namespace DefuseOrLose
{
    public abstract class ButtonPuzzle : MonoBehaviour
    {
        protected ButtonPuzzleManager buttonPuzzleManager;

        protected virtual void Start()
        {
            buttonPuzzleManager = GetComponent<ButtonPuzzleManager>();
            GetComponent<Button>().onClick.AddListener(ExecutePuzzle);
        }

        protected abstract void ExecutePuzzle();
    }
}
