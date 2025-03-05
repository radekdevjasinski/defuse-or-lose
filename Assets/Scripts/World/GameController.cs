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
    [Header("Sounds")]
    public GameObject explodeSound;
    public GameObject winSound;
    public GameObject loseSound;
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
        AudioManager.Instance.PlaySound(loseSound);
        bombController.AddStrike();
        if (bombController.strikes >= maxStrikes)
        {
            LoseGame();
        }
        screenDistributor.ReloadPuzzle();
    }
    public void WinPuzzle()
    {
        AudioManager.Instance.PlaySound(winSound);
        int winIndex = screenDistributor.currentPuzzleIndex;
        screenDistributor.NextPuzzleRight();
        puzzleController.activePuzzles[winIndex].isCompleted = true;
        screenDistributor.SpawnIcons();
        CheckPuzzles();
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
        bombController.timerRunning = false;
        AudioManager.Instance.StopAllSounds();
        StartCoroutine(LoseGameCoroutine());

    }
    IEnumerator LoseGameCoroutine()
    {
        yield return new WaitForSeconds(1f);
        loseScreen.GetComponent<Animator>().SetTrigger("lose");
        AudioManager.Instance.PlaySound(explodeSound);
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0f;

    }
    public void WinGame()
    {
        winScreen.SetActive(true);
        bombController.timerRunning = false;
        winScreen.GetComponent<Animator>().SetTrigger("win");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
    }
}
