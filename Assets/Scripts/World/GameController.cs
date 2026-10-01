using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace DefuseOrLose
{
    public class GameController : MonoBehaviour
    {
        private const float ExplosionDelaySeconds = 1f;
        private const float FreezeDelaySeconds = 1.5f;

        public static GameController Instance { get; private set; }

        [Header("Refs")]
        [SerializeField] private PuzzleController puzzleController;
        [SerializeField] private BombController bombController;
        [SerializeField] private ScreenDistributor screenDistributor;
        [SerializeField] private int maxStrikes = 3;

        [Header("Screens")]
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject loseScreen;

        [Header("Sounds")]
        [SerializeField] private GameObject explodeSound;
        [SerializeField] private GameObject winSound;
        [SerializeField] private GameObject loseSound;

        private bool isGameOver = false;

        public ScreenDistributor ScreenDistributor => screenDistributor;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            ApplyLevelData();
        }

        void Update()
        {
            if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                SceneLoader.LoadMenu();
            }
        }

        public void LosePuzzle()
        {
            if (isGameOver)
                return;

            AudioManager.Instance.PlaySound(loseSound);
            bombController.AddStrike();
            if (bombController.Strikes >= maxStrikes)
            {
                LoseGame();
                return;
            }
            screenDistributor.ReloadPuzzle();
        }

        public void WinPuzzle()
        {
            if (isGameOver)
                return;

            AudioManager.Instance.PlaySound(winSound);
            puzzleController.ActivePuzzles[screenDistributor.CurrentPuzzleIndex].IsCompleted = true;
            screenDistributor.NextPuzzleRight();
            screenDistributor.RefreshIcons();
            if (puzzleController.AreAllPuzzlesCompleted)
            {
                WinGame();
            }
        }

        public void LoseGame()
        {
            if (isGameOver)
                return;

            isGameOver = true;
            loseScreen.SetActive(true);
            bombController.StopTimer();
            AudioManager.Instance.StopAllSounds();
            StartCoroutine(LoseGameCoroutine());
        }

        void WinGame()
        {
            isGameOver = true;
            winScreen.SetActive(true);
            bombController.StopTimer();
            winScreen.GetComponent<Animator>().SetTrigger("win");
        }

        void ApplyLevelData()
        {
            if (!LevelCatalog.TryFindForScene(gameObject.scene.buildIndex, out LevelData levelData))
                return;

            maxStrikes = levelData.maxStrikes;
            bombController.SetTimeLimit(levelData.TimeLimitSeconds);
            puzzleController.SetPuzzleCount(levelData.puzzlesCount);
        }

        IEnumerator LoseGameCoroutine()
        {
            yield return new WaitForSeconds(ExplosionDelaySeconds);
            loseScreen.GetComponent<Animator>().SetTrigger("lose");
            AudioManager.Instance.PlaySound(explodeSound);
            yield return new WaitForSeconds(FreezeDelaySeconds);
            Time.timeScale = 0f;
        }
    }
}
