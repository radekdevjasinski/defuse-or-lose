using UnityEngine;

public abstract class PuzzleBase : MonoBehaviour
{
    public PuzzleController puzzleController;

    public abstract void Initialize();

    protected virtual void OnComplete()
    {
        GameController.instance.WinPuzzle();
    }
    protected virtual void OnFail()
    {
        GameController.instance.LosePuzzle();
    }
}
