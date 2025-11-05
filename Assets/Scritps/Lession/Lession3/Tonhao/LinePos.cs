using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(LineRenderer))]
public class LinePos : MonoBehaviour
{
    public string lineName;
    public List<Transform> posList;
    public List<LinePos> linePosChildList;
    public Material material;
    public Vector2 vector2TextureScale;
    private LineRenderer lineRenderer;
    private int stage = 1;
    public GameObject textDisplay;
    public GameObject arrowhead;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Init();
    }
    private void Start()
    {
        // StartCoroutine(Draw());
    }
    private void OnEnable()
    {
        StartCoroutine(Draw());
    }
    private void OnDisable()
    {
        lineRenderer.positionCount = 0;
        arrowhead.SetActive(false);

        if (textDisplay != null)
        {
            textDisplay.SetActive(false);
        }
        
        if (linePosChildList.Count > 0)
        {
            foreach (LinePos t in linePosChildList)
                if (t != null)
                    t.enabled = false;
        }
    }
    void Init()
    {
        // lineRenderer.positionCount = 2;
        // lineRenderer.startColor = Color.red;
        // lineRenderer.endColor = Color.red;
        lineRenderer.material = material;
        lineRenderer.startWidth = 0.8f;
        lineRenderer.endWidth = 1f;
        lineRenderer.textureScale = vector2TextureScale;
        // lineRenderer.useWorldSpace = false;
        // 
        arrowhead.GetComponent<SpriteRenderer>().color = lineRenderer.startColor;
    }

    IEnumerator Draw()
    {
        arrowhead.SetActive(true);
        lineRenderer.positionCount = posList.Count;
        for (int i = 0; i < posList.Count - 1; i++)
        {
            // lineRenderer.positionCount++;
            int j = i + 1;
            // Debug.Log("" + stage + " " + j);
            yield return StartCoroutine(MoveObject(i, j, 3f));
            if (linePosChildList.Count > 0 && j == stage)
            {
                foreach (LinePos t in linePosChildList)
                    if (t != null)
                        t.enabled = true;
            }
        }
        if (textDisplay != null)
        {
            textDisplay.SetActive(true);
        }
        // StopAllCoroutines();
    }
    IEnumerator MoveObject(int start, int end, float duration)
    {
        float elapsedTime = 0.0f;
        Vector3 v3 = Vector3.zero;
        int frameCount = 3; // Thêm số lượng frame giảm giật lag do quá tải tính toán trong 1 frame

        while (elapsedTime < duration)
        {
            if (frameCount == 0)
            {
                v3 = Vector3.Lerp(posList[start].position, posList[end].position, elapsedTime / duration);

                lineRenderer.SetPosition(start, posList[start].position);
                lineRenderer.SetPosition(end, v3);

                if (arrowhead != null)
                {
                    arrowhead.transform.position = v3;

                    Vector3 direction = v3 - posList[start].position;
                    if (direction != Vector3.zero)
                    {
                        arrowhead.transform.rotation = Quaternion.LookRotation(direction);
                        arrowhead.transform.Rotate(-90, 0, 0);

                        if (elapsedTime >= duration)
                        {
                            arrowhead.SetActive(false);
                        }
                    }
                }
                frameCount = 4;
            }

            elapsedTime += Time.deltaTime;
            frameCount--;

            yield return null;  // Chờ một frame
        }

        lineRenderer.SetPosition(start, posList[start].position);
        lineRenderer.SetPosition(end, v3);
        arrowhead.SetActive(false);
        // if (arrowhead != null)
        // {
        //     arrowhead.transform.position = posList[end].position;

        //     Vector3 finalDirection = posList[end].position - posList[start].position;
        //     arrowhead.transform.rotation = Quaternion.LookRotation(finalDirection);
        //     arrowhead.transform.Rotate(-90, 0, 0);
        // }
    }
}
