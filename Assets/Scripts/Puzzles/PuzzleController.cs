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
    public int puzzles;
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
        List<GameObject> randomPrefabs = new List<GameObject>(puzzlePrefabs);

        for (int i = 0; i < puzzles; i++)
        {
            int randomIndex = Random.Range(0, randomPrefabs.Count);
            GameObject selectedPrefab = randomPrefabs[randomIndex];
            Puzzle puzzle = new Puzzle(selectedPrefab);
            activePuzzles.Add(puzzle);        
            randomPrefabs.RemoveAt(randomIndex);
        }
    }
}
