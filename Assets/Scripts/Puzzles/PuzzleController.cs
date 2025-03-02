using UnityEngine;
using System.Collections.Generic;

public class Puzzle
{
    public GameObject prefab;
    public bool isCompleted = false;
    public Puzzle(GameObject prefab)
    {
        this.prefab = prefab;
    }
}
public class PuzzleController : MonoBehaviour
{
    public GameObject[] puzzlePrefabs;
    public List<Puzzle> activePuzzles = new List<Puzzle>();
    private ScreenDistributor screenDistributor;

    void Start()
    {
        screenDistributor = GetComponent<ScreenDistributor>();
        ChoosePuzzles();
        screenDistributor.DisplayPuzzle(activePuzzles[0].prefab);
        screenDistributor.SpawnIcons();
    }


    void ChoosePuzzles()
    {
        if (puzzlePrefabs.Length == 0) return;
        
        activePuzzles.Clear(); 

        for (int i = 0; i < puzzlePrefabs.Length; i++)
        {
            GameObject selectedPrefab = puzzlePrefabs[i];
            Puzzle puzzle = new Puzzle(selectedPrefab);
            activePuzzles.Add(puzzle);        
        }
    }
}
