using System.Linq;
using UnityEngine;

namespace DefuseOrLose
{
    public static class LevelCatalog
    {
        private const string LevelsResourcePath = "Levels";

        public static LevelData[] LoadLevelsInSceneOrder()
        {
            return Resources.LoadAll<LevelData>(LevelsResourcePath)
                .OrderBy(levelData => levelData.sceneIndex)
                .ToArray();
        }

        public static bool TryFindForScene(int buildIndex, out LevelData levelData)
        {
            levelData = Resources.LoadAll<LevelData>(LevelsResourcePath)
                .FirstOrDefault(candidate => candidate.sceneIndex == buildIndex);
            return levelData != null;
        }
    }
}
