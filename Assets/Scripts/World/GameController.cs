using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    [Header("Refs")]
    public static GameController instance;
    public PuzzleController puzzleController;
    public BombController bombController;
    public ScreenDistributor screenDistributor;
    public int maxStrikes = 3;

    [Header("Screens")]
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject explodeSound;
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
        if (bombController.strikes >= maxStrikes)
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
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        loseScreen.SetActive(true);
        StartCoroutine(LoseGameCoroutine());

    }
    IEnumerator LoseGameCoroutine()
    {
        yield return new WaitForSeconds(1f);
        loseScreen.GetComponent<Animator>().SetTrigger("lose");
        AudioManager.Instance.PlaySound(explodeSound);
    }
    public void WinGame()
    {
        winScreen.SetActive(true);
        winScreen.GetComponent<Animator>().SetTrigger("win");
    }
    void Update()
    {
        if (bombController.timeRemaining <= 0f && !bombController.timerRunning)
        {
            LoseGame();
        }
    }
}
