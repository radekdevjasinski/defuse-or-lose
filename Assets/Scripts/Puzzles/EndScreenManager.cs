using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    public Button returnToMenuButton;
    public Button exitButton;

    private void Start()
    {
        if (returnToMenuButton != null)
        {
            returnToMenuButton.onClick.RemoveAllListeners();
            returnToMenuButton.onClick.AddListener(ReturnToMenu);
        }

        if (exitButton != null)
        {
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(ExitGame);
        }
    }

    void ReturnToMenu()
    {
        Debug.Log("Return to Menu clicked.");
        SceneManager.LoadScene(0);
    }

    void ExitGame()
    {
        Debug.Log("Exit Game clicked.");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
