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
    public string folderPath = "Puzzles";
    public int numberOfPuzzles = 6;
    private List<GameObject> puzzlePrefabs = new List<GameObject>();
    public List<Puzzle> activePuzzles = new List<Puzzle>();
    private ScreenDistributor screenDistributor;

    void Start()
    {
        screenDistributor = GetComponent<ScreenDistributor>();
        LoadPrefabs();
        ChoosePuzzles();
        screenDistributor.DisplayPuzzle(activePuzzles[0].prefab);
        screenDistributor.SpawnIcons();
    }

    void LoadPrefabs()
    {
        Object[] loadedPrefabs = Resources.LoadAll(folderPath, typeof(GameObject));

        foreach (Object obj in loadedPrefabs)
        {
            puzzlePrefabs.Add(obj as GameObject);
        }

        if (puzzlePrefabs.Count == 0)
        {
            Debug.LogError("Nie znaleziono prefabów w folderze: " + folderPath);
        }
    }

    void ChoosePuzzles()
    {
        if (puzzlePrefabs.Count == 0) return;
        
        activePuzzles.Clear(); 
        List<GameObject> availablePrefabs = new List<GameObject>(puzzlePrefabs);

        for (int i = 0; i < numberOfPuzzles && availablePrefabs.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, availablePrefabs.Count);
            GameObject selectedPrefab = availablePrefabs[randomIndex];
            Puzzle puzzle = new Puzzle(selectedPrefab);
            activePuzzles.Add(puzzle);
            availablePrefabs.RemoveAt(randomIndex); 
        }
    }
}
