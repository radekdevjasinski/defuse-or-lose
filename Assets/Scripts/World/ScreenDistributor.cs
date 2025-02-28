using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class ScreenDistributor : MonoBehaviour
{
    [Header("Refs")]
    public PuzzleController puzzleController;
    public Transform iconsContainer;
    public GameObject puzzleIconEmpty;
    public GameObject puzzleIconFilled;

    public int currentPuzzleIndex = 0;
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
    public void ReloadPuzzle()
    {
        DisplayPuzzle(puzzleController.activePuzzles[currentPuzzleIndex].prefab);
    }
    public void NextPuzzleRight()
    {
        List<Puzzle> uncompleted = new List<Puzzle>();
        foreach (Puzzle puzzle in puzzleController.activePuzzles)
        {
            if (!puzzle.isCompleted)
            {
                uncompleted.Add(puzzle);
            }
        }
        if (uncompleted.Count <= 1) return;

        do
        {
            currentPuzzleIndex++;
            if (currentPuzzleIndex >= puzzleController.activePuzzles.Count)
            {
                currentPuzzleIndex = 0;
            }
        } while (puzzleController.activePuzzles[currentPuzzleIndex].isCompleted);

        DisplayPuzzle(puzzleController.activePuzzles[currentPuzzleIndex].prefab);
        SpawnIcons();
    }

    public void NextPuzzleLeft()
    {
        List<Puzzle> uncompleted = new List<Puzzle>();
        foreach (Puzzle puzzle in puzzleController.activePuzzles)
        {
            if (!puzzle.isCompleted)
            {
                uncompleted.Add(puzzle);
            }
        }
        if (uncompleted.Count <= 1) return;

        do
        {
            currentPuzzleIndex--;
            if (currentPuzzleIndex < 0)
            {
                currentPuzzleIndex = puzzleController.activePuzzles.Count - 1;
            }
        } while (puzzleController.activePuzzles[currentPuzzleIndex].isCompleted);

        DisplayPuzzle(puzzleController.activePuzzles[currentPuzzleIndex].prefab);
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
            GameObject icon;
            if (puzzleController.activePuzzles[i].isCompleted)
                icon = Instantiate(puzzleIconFilled, iconsContainer);
            else
                icon = Instantiate(puzzleIconEmpty, iconsContainer);


            if (i == currentPuzzleIndex)
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
