using UnityEngine;
using UnityEngine.UI;

namespace DefuseOrLose
{
    public class EndScreenManager : MonoBehaviour
    {
        [SerializeField] private Button returnToMenuButton;
        [SerializeField] private Button exitButton;

        private void Start()
        {
            if (returnToMenuButton != null)
            {
                returnToMenuButton.onClick.RemoveAllListeners();
                returnToMenuButton.onClick.AddListener(SceneLoader.LoadMenu);
            }

            if (exitButton != null)
            {
                exitButton.onClick.RemoveAllListeners();
                exitButton.onClick.AddListener(SceneLoader.QuitGame);
            }
        }
    }
}
