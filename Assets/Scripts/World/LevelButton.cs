using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DefuseOrLose
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelNameText;
        [SerializeField] private TMP_Text minutesText;
        [SerializeField] private TMP_Text strikesText;
        [SerializeField] private TMP_Text puzzlesText;
        [SerializeField] private Button button;

        private int sceneIndex;

        public void Setup(LevelData levelData)
        {
            levelNameText.text = levelData.levelName;
            minutesText.text = Mathf.RoundToInt(levelData.timeLimitMinutes) + " Minutes";
            strikesText.text = levelData.maxStrikes + " Strikes";
            puzzlesText.text = levelData.puzzlesCount + " Puzzles";
            sceneIndex = levelData.sceneIndex;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(LoadLevel);
        }

        void LoadLevel()
        {
            SceneLoader.LoadScene(sceneIndex);
        }
    }
}
