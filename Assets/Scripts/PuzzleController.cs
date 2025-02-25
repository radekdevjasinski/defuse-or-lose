using UnityEngine;
using System.Collections.Generic;

public class PuzzleController : MonoBehaviour
{
    public string folderPath = "Puzzles"; // Ścieżka w folderze Resources
    public int numberOfPuzzles = 6; // Ile prefabów utworzyć
    private List<GameObject> puzzlePrefabs = new List<GameObject>();
    private ScreenDistributor screenDistributor;

    void Start()
    {
        screenDistributor = GetComponent<ScreenDistributor>();
        LoadPrefabs();
        SpawnPuzzles();
        screenDistributor.DistributeChildren();
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

    void SpawnPuzzles()
    {
        if (puzzlePrefabs.Count == 0) return;

        for (int i = 0; i < numberOfPuzzles; i++)
        {
            GameObject randomPrefab = puzzlePrefabs[Random.Range(0, puzzlePrefabs.Count)];
            GameObject spawnedPuzzle = Instantiate(randomPrefab, this.transform);
            spawnedPuzzle.name = spawnedPuzzle.name + "_" + i;
        }
    }
}
