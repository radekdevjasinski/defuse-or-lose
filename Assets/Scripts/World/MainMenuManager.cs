using UnityEngine;
using UnityEngine.UI;

namespace DefuseOrLose
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Panel Menu")]
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject levelSelectPanel;
        [SerializeField] private GameObject creditsPanel;

        [Header("Main Menu Buttons")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button quitButton;

        [Header("Level Selection Buttons")]
        [SerializeField] private Button backButton;

        [Header("Credits Panel Button")]
        [SerializeField] private Button creditsBackButton;

        private void Start()
        {
            ShowPanel(mainPanel);

            playButton.onClick.AddListener(() => ShowPanel(levelSelectPanel));
            optionsButton.onClick.AddListener(() => ShowPanel(creditsPanel));
            quitButton.onClick.AddListener(SceneLoader.QuitGame);
            backButton.onClick.AddListener(() => ShowPanel(mainPanel));
            creditsBackButton.onClick.AddListener(() => ShowPanel(mainPanel));
        }

        void ShowPanel(GameObject panel)
        {
            mainPanel.SetActive(panel == mainPanel);
            levelSelectPanel.SetActive(panel == levelSelectPanel);
            creditsPanel.SetActive(panel == creditsPanel);
        }
    }
}
