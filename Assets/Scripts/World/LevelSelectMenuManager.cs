using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class LevelSelectMenuManager : MonoBehaviour
{
    public Transform buttonContainer;         // Kontener (pusty obiekt z Grid Layout Group)
    public GameObject levelButtonPrefab;        // Prefab przycisku poziomu

    void Start()
    {
        // Ładujemy wszystkie LevelData z folderu Resources/Levels
        LevelData[] levelDatas = Resources.LoadAll<LevelData>("Levels");
        // Sortujemy według sceneIndex, aby przyciski były uporządkowane
        var sortedLevels = levelDatas.OrderBy(ld => ld.sceneIndex).ToArray();

        foreach (LevelData ld in sortedLevels)
        {
            GameObject buttonObj = Instantiate(levelButtonPrefab, buttonContainer);
            LevelButton levelButton = buttonObj.GetComponent<LevelButton>();
            if (levelButton != null)
            {
                levelButton.Setup(ld.levelName, Mathf.RoundToInt(ld.timeRemaining), ld.maxStrikes, ld.puzzlesCount, ld.sceneIndex);
            }
            else
            {
                Debug.LogError("Prefab LevelButton nie posiada skryptu LevelButton!");
            }
        }
    }
}
