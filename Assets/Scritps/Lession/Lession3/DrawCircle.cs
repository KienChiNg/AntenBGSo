using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawCircle : MonoBehaviour
{
    public float radius = 2f; // Bán kính đường tròn
    public int segments = 50; // Số đoạn tạo nên đường tròn

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = segments + 1; // Số điểm = số đoạn + 1 để đóng kín đường tròn
        // lineRenderer.loop = true; // Đóng kín đường tròn

        SetInitLineRenderer();
        CreatePoints();
    }
     void SetInitLineRenderer()
    {
        lineRenderer.startWidth = 0.04f;
        lineRenderer.endWidth = 0.04f;
    }

    void CreatePoints()
    {
        float angle = 2 * Mathf.PI / segments;
        for (int i = 0; i < (segments + 1)/10; i++)
        {
            float x = radius * Mathf.Cos(i * angle);
            float y = radius * Mathf.Sin(i * angle);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}
