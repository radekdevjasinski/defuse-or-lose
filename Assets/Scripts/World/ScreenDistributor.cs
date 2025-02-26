using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScreenDistributor : MonoBehaviour
{
    [Header("Refs")]
    public PuzzleController puzzleController;
    public Transform iconsContainer;
    public GameObject puzzleIconEmpty;

    private int currentPuzzleIndex = 0;
    private GameObject activePuzzle;
    [Header("Icons")]
    public List<GameObject> puzzleIcons = new();

    private void Start()
    {
        puzzleController = GetComponent<PuzzleController>();
    }
    public void DisplayPuzzle(GameObject puzzlePrefab)
    {
        if (activePuzzle != null)
        {
            Destroy(activePuzzle);
        }

        activePuzzle = Instantiate(puzzlePrefab, transform);
    }

    public void NextPuzzleRight()
    {
        if (puzzleController.activePuzzles.Count == 0) return;

        currentPuzzleIndex++;
        if (currentPuzzleIndex >= puzzleController.activePuzzles.Count)
        {
            currentPuzzleIndex = 0;
        }

        DisplayPuzzle(puzzleController.activePuzzles[currentPuzzleIndex]);
        SpawnIcons();
    }

    public void NextPuzzleLeft()
    {
        if (puzzleController.activePuzzles.Count == 0) return;

        currentPuzzleIndex--;
        if (currentPuzzleIndex < 0)
        {
            currentPuzzleIndex = puzzleController.activePuzzles.Count - 1;
        }

        DisplayPuzzle(puzzleController.activePuzzles[currentPuzzleIndex]);
        SpawnIcons();

    }
    public void SpawnIcons()
    {
        foreach (Transform child in iconsContainer)
        {
            Destroy(child.gameObject);
        }
        puzzleIcons.Clear();

        for (int i = 0; i < puzzleController.activePuzzles.Count; i++)
        {
            GameObject icon = Instantiate(puzzleIconEmpty, iconsContainer);
            if(i == currentPuzzleIndex)
            {
                icon.transform.localScale = Vector3.one * 1.5f;
            }
            else
            {
                icon.transform.localScale = Vector3.one; 
            }
            puzzleIcons.Add(icon);

        }
    }
}
