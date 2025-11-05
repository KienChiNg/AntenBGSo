using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(LineRenderer))]
public class BezierCurve : MonoBehaviour
{
    public Transform point0, point1, point2, point3;
    public int segmentCount = 50;
    public GameObject arrow;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = (segmentCount + 1);
        // lineRenderer.useWorldSpace = false;
        SetInitLineRenderer();
    }
    private void Update()
    {
        CreateBezierCurve();

    }
    void CreateBezierCurve()
    {
        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            Vector3 position = CalculateBezierPoint(t, point0.position, point1.position, point2.position, point3.position);
            lineRenderer.SetPosition(i, position);
        }

        RotateArrow();
    }
    void SetInitLineRenderer()
    {
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
    }
    void RotateArrow()
    {
        Vector3 startPos = point2.localPosition - point3.localPosition;
        Vector3 endPos = point2.localPosition - new Vector3(0, point2.localPosition.y, 0);

        float alphaAngle = Vector3.Angle(startPos, endPos) + 90;
        // Debug.Log(alphaAngle);
        arrow.transform.localRotation = Quaternion.Euler(0, 0, alphaAngle);
    }
    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0; // u^3 * p0
        p += 3 * uu * t * p1; // 3 * u^2 * t * p1
        p += 3 * u * tt * p2; // 3 * u * t^2 * p2
        p += ttt * p3; // t^3 * p3

        return p;
    }
}
