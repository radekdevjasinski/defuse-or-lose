using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefuseOrLose
{
    public class ScreenDistributor : MonoBehaviour
    {
        private const float CurrentIconScale = 1.5f;
        private const int StepRight = 1;
        private const int StepLeft = -1;

        [Header("Refs")]
        [SerializeField] private Transform iconsContainer;
        [SerializeField] private GameObject puzzleIconEmpty;
        [SerializeField] private GameObject puzzleIconFilled;

        private readonly List<GameObject> emptyIcons = new List<GameObject>();
        private readonly List<GameObject> filledIcons = new List<GameObject>();
        private PuzzleController puzzleController;

        public event Action<GameObject> PuzzleDisplayed;

        public int CurrentPuzzleIndex { get; private set; }
        public GameObject ActivePuzzle { get; private set; }

        private void Awake()
        {
            puzzleController = GetComponent<PuzzleController>();
            foreach (Transform placeholderIcon in iconsContainer)
            {
                Destroy(placeholderIcon.gameObject);
            }
        }

        public void DisplayPuzzle(GameObject puzzlePrefab)
        {
            if (ActivePuzzle != null)
            {
                Destroy(ActivePuzzle);
            }
            ActivePuzzle = Instantiate(puzzlePrefab, transform);
            PuzzleDisplayed?.Invoke(ActivePuzzle);
        }

        public void ReloadPuzzle()
        {
            DisplayPuzzle(puzzleController.ActivePuzzles[CurrentPuzzleIndex].Prefab);
        }

        public void NextPuzzleRight()
        {
            StepPuzzle(StepRight);
        }

        public void NextPuzzleLeft()
        {
            StepPuzzle(StepLeft);
        }

        public void RefreshIcons()
        {
            IReadOnlyList<Puzzle> puzzles = puzzleController.ActivePuzzles;
            EnsureIconPool(puzzles.Count);

            for (int i = 0; i < puzzles.Count; i++)
            {
                Vector3 iconScale = i == CurrentPuzzleIndex ? Vector3.one * CurrentIconScale : Vector3.one;
                emptyIcons[i].SetActive(!puzzles[i].IsCompleted);
                emptyIcons[i].transform.localScale = iconScale;
                filledIcons[i].SetActive(puzzles[i].IsCompleted);
                filledIcons[i].transform.localScale = iconScale;
            }
        }

        private void StepPuzzle(int direction)
        {
            if (!puzzleController.HasUncompletedPuzzleOtherThan(CurrentPuzzleIndex))
                return;

            IReadOnlyList<Puzzle> puzzles = puzzleController.ActivePuzzles;
            do
            {
                CurrentPuzzleIndex = (CurrentPuzzleIndex + direction + puzzles.Count) % puzzles.Count;
            } while (puzzles[CurrentPuzzleIndex].IsCompleted);

            DisplayPuzzle(puzzles[CurrentPuzzleIndex].Prefab);
            RefreshIcons();
        }

        private void EnsureIconPool(int puzzleCount)
        {
            while (emptyIcons.Count < puzzleCount)
            {
                emptyIcons.Add(Instantiate(puzzleIconEmpty, iconsContainer));
                filledIcons.Add(Instantiate(puzzleIconFilled, iconsContainer));
            }
        }
    }
}
