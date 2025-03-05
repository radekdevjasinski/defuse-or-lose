using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Level Data", order = 51)]
public class LevelData : ScriptableObject
{
    public int sceneIndex;
    public string levelName;
    public float timeRemaining;
    public int maxStrikes;
    public int puzzlesCount;
}
