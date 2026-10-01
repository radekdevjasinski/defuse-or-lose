using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace DefuseOrLose
{
    public class PuzzleController : MonoBehaviour
    {
        [SerializeField] private GameObject[] puzzlePrefabs;
        [SerializeField] private int puzzles;

        private readonly List<Puzzle> activePuzzles = new List<Puzzle>();
        private ScreenDistributor screenDistributor;

        public IReadOnlyList<Puzzle> ActivePuzzles => activePuzzles;

        public bool AreAllPuzzlesCompleted => activePuzzles.All(puzzle => puzzle.IsCompleted);

        void Awake()
        {
            screenDistributor = GetComponent<ScreenDistributor>();
        }

        void Start()
        {
            ChoosePuzzles();
            if (activePuzzles.Count == 0)
            {
                Debug.LogError("PuzzleController: no puzzles to display, check puzzlePrefabs and puzzles count.");
                return;
            }
            screenDistributor.DisplayPuzzle(activePuzzles[0].Prefab);
            screenDistributor.RefreshIcons();
        }

        public void SetPuzzleCount(int puzzleCount)
        {
            puzzles = puzzleCount;
        }

        public bool HasUncompletedPuzzleOtherThan(int puzzleIndex)
        {
            return activePuzzles.Where((puzzle, index) => index != puzzleIndex && !puzzle.IsCompleted).Any();
        }

        void ChoosePuzzles()
        {
            int puzzleCount = Mathf.Min(puzzles, puzzlePrefabs.Length);
            if (puzzleCount < puzzles)
            {
                Debug.LogWarning($"PuzzleController: requested {puzzles} puzzles but only {puzzlePrefabs.Length} prefabs are assigned.");
            }

            activePuzzles.Clear();
            List<GameObject> remainingPrefabs = new List<GameObject>(puzzlePrefabs);

            for (int i = 0; i < puzzleCount; i++)
            {
                int randomIndex = Random.Range(0, remainingPrefabs.Count);
                activePuzzles.Add(new Puzzle(remainingPrefabs[randomIndex]));
                remainingPrefabs.RemoveAt(randomIndex);
            }
        }
    }
}
