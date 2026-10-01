using UnityEngine;
using UnityEngine.Serialization;

namespace DefuseOrLose
{
    [CreateAssetMenu(fileName = "New Level Data", menuName = "Level Data", order = 51)]
    public class LevelData : ScriptableObject
    {
        private const float SecondsPerMinute = 60f;

        public int sceneIndex;
        public string levelName;
        [FormerlySerializedAs("timeRemaining")] public float timeLimitMinutes;
        public int maxStrikes;
        public int puzzlesCount;

        public float TimeLimitSeconds => timeLimitMinutes * SecondsPerMinute;
    }
}
