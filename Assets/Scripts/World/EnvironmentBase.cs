using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace DefuseOrLose
{
    public abstract class EnvironmentBase : MonoBehaviour
    {
        [SerializeField] protected Light2D globalLight;

        protected static WaitForSeconds WaitForRandomizedInterval(float interval, float minOffset, float maxOffset)
        {
            return new WaitForSeconds(Mathf.Max(0f, interval + Random.Range(minOffset, maxOffset)));
        }
    }
}
