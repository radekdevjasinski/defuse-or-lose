using UnityEngine;

namespace DefuseOrLose
{
    public class LevelSelectMenuManager : MonoBehaviour
    {
        [SerializeField] private Transform buttonContainer;
        [SerializeField] private GameObject levelButtonPrefab;

        void Start()
        {
            foreach (LevelData levelData in LevelCatalog.LoadLevelsInSceneOrder())
            {
                GameObject buttonObject = Instantiate(levelButtonPrefab, buttonContainer);
                LevelButton levelButton = buttonObject.GetComponent<LevelButton>();
                if (levelButton == null)
                {
                    Debug.LogError("LevelSelectMenuManager: level button prefab has no LevelButton component.");
                    return;
                }
                levelButton.Setup(levelData);
            }
        }
    }
}
