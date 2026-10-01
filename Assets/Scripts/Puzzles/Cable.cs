using UnityEngine;

namespace DefuseOrLose
{
    public class Cable : MonoBehaviour
    {
        private const int StartPointIndex = 0;
        private const int EndPointIndex = 1;
        private const int RoundingVertices = 10;

        private LineRenderer lineRenderer;

        void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.numCapVertices = RoundingVertices;
            lineRenderer.numCornerVertices = RoundingVertices;
        }

        public void SetStart(Vector3 position)
        {
            lineRenderer.SetPosition(StartPointIndex, position);
        }

        public void SetEnd(Vector3 position)
        {
            lineRenderer.SetPosition(EndPointIndex, position);
        }
    }
}
