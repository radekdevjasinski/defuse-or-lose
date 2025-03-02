using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButton : MonoBehaviour
{
    public TMP_Text levelNameText;
    public TMP_Text minutesText;
    public TMP_Text strikesText;
    public TMP_Text puzzlesText;
    public Button button;

    private int sceneIndex;

    public void Setup(string levelName, int minutes, int strikes, int puzzles, int sceneIndex)
    {
        levelNameText.text = levelName;
        minutesText.text = minutes + " Minutes";
        strikesText.text = strikes + " Strikes";
        puzzlesText.text = puzzles + " Puzzles";
        this.sceneIndex = sceneIndex;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => LoadLevel());
    }

    void LoadLevel()
    {
        Debug.Log("Loading level: " + levelNameText.text + " (Scene index: " + sceneIndex + ")");
        SceneManager.LoadScene(sceneIndex);
    }
}
