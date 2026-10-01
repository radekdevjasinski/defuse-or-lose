using System.Diagnostics;

namespace DefuseOrLose
{
    public static class EditorLog
    {
        [Conditional("UNITY_EDITOR")]
        public static void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }
    }
}
