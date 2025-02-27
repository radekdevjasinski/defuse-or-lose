using UnityEngine;

public class Cable : MonoBehaviour
{
    private LineRenderer lr;
    [HideInInspector] public Vector3 startPos;
    [HideInInspector] public Vector3 endPos;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.numCapVertices = 10;
        lr.numCornerVertices = 10;
        Debug.Log("[Cable] Awake: LineRenderer initialized.");
    }

    void Update()
    {
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
    }
}
