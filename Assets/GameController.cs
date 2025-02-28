using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public PuzzleController puzzleController;
    public BombController bombController;
    public ScreenDistributor screenDistributor;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LosePuzzle()
    {
        bombController.AddStrike();
        if (bombController.strikes >= 3)
        {
            LoseGame();
        }
        screenDistributor.ReloadPuzzle();
        Debug.Log("You lost the puzzle!");
    }
    public void WinPuzzle()
    {
        int winIndex = screenDistributor.currentPuzzleIndex;
        screenDistributor.NextPuzzleRight();
        puzzleController.activePuzzles[winIndex].isCompleted = true;
        screenDistributor.SpawnIcons();
        CheckPuzzles();
        Debug.Log("You won the puzzle!");
    }
    void CheckPuzzles()
    {
        foreach (Puzzle puzzle in puzzleController.activePuzzles)
        {
            if (!puzzle.isCompleted)
            {
                return;
            }
        }
        WinGame();
    }
    public void LoseGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void WinGame()
    {
        Time.timeScale = 0;
        Debug.Log("You won the game!");
    }
    void Update()
    {
        if (bombController.timeRemaining <= 0f && !bombController.timerRunning)
        {
            LoseGame();
        }
    }
}
