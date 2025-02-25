using UnityEngine;

public abstract class PuzzleBase : MonoBehaviour
{
    // Czy zadanie jest ukończone?
    public bool isCompleted { get; protected set; } = false;

    // Przygotowanie zadania do działania
    public abstract void Initialize();
}
