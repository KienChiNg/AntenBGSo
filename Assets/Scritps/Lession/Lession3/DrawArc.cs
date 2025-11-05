using UnityEngine;

public class DrawArc : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform[] points; // Mảng các điểm sẽ nối với nhau

    void Start()
    {
        SetInitLineRenderer();
        lineRenderer.positionCount = points.Length; // Số điểm trong LineRenderer
        // lineRenderer.useWorldSpace = false;
        // Gán vị trí các điểm cho LineRenderer
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }
    void SetInitLineRenderer()
    {
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
    }

}
