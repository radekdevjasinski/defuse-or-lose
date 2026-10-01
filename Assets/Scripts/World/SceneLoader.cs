using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefuseOrLose
{
    public static class SceneLoader
    {
        private const int MenuSceneIndex = 0;

        public static void LoadMenu()
        {
            LoadScene(MenuSceneIndex);
        }

        public static void LoadScene(int buildIndex)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(buildIndex);
        }

        public static void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
