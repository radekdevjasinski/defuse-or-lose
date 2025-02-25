using UnityEngine;

public abstract class PuzzleBase : MonoBehaviour
{
    public bool isCompleted { get; protected set; } = false;

    public abstract void Initialize();

    protected virtual void OnComplete()
    {
        isCompleted = true;
        Debug.Log(gameObject.name + " completed!");
    }
}
