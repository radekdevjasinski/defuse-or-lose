using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panel Menu")]
    public GameObject mainPanel;
    public GameObject levelSelectPanel;

    [Header("Main Menu Buttons")]
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;

    [Header("Level Selection Buttons")]
    public Button[] levelButtons;
    public Button backButton;

    private void Start()
    {
        Debug.Log("MainMenuManager Start() uruchomiony");
        mainPanel.SetActive(true);
        levelSelectPanel.SetActive(false);

        playButton.onClick.AddListener(OnPlayClicked);
        quitButton.onClick.AddListener(OnQuitClicked);

        backButton.onClick.AddListener(OnBackClicked);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i;
            levelButtons[i].onClick.AddListener(() => OnLevelButtonClicked(index));
        }
    }

    void OnPlayClicked()
    {
        Debug.Log("Play button clicked. Switching to level selection panel.");
        mainPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    void OnQuitClicked()
    {
        Debug.Log("Quit button clicked. Exiting game.");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void OnBackClicked()
    {
        Debug.Log("Back button clicked. Returning to main menu panel.");
        levelSelectPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    void OnLevelButtonClicked(int index)
    {
        Debug.Log("Level button clicked: " + index);
        SceneManager.LoadScene(index + 1);
    }
}
