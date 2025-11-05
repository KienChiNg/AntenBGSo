using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    public Transform origin;
    public Transform destination;
    LineRenderer lr;
    public List<line> linePosChildList;
    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    // void Update()
    // {
    //     lr.SetPosition(0, origin.position);
    //     lr.SetPosition(1, destination.position);
    // }
    private void OnEnable()
    {
        StartCoroutine(Draw());
    }
    private void OnDisable()
    {
        lr.positionCount = 0;

        if (linePosChildList.Count > 0)
        {
            foreach (line t in linePosChildList)
                if (t != null)
                    t.enabled = false;
        }
    }
    IEnumerator Draw()
    {
        lr.positionCount = 2;

        yield return StartCoroutine(MoveObject(1f));
        if (linePosChildList.Count > 0)
        {
            foreach (line t in linePosChildList)
                if (t != null)
                    t.enabled = true;
        }
    }
    IEnumerator MoveObject(float duration)
    {
        float elapsedTime = 0.0f;
        Vector3 v3 = Vector3.zero;
        int frameCount = 3; // Thêm số lượng frame giảm giật lag do quá tải tính toán trong 1 frame

        while (elapsedTime < duration)
        {
            if (frameCount == 0)
            {
                v3 = Vector3.Lerp(origin.position, destination.position, elapsedTime / duration);

                lr.SetPosition(0, origin.position);
                lr.SetPosition(1, v3);

                frameCount = 4;
            }

            elapsedTime += Time.deltaTime;
            frameCount--;

            yield return null;  // Chờ một frame
        }

        lr.SetPosition(0, origin.position);
        lr.SetPosition(1, v3);
        // if (arrowhead != null)
        // {
        //     arrowhead.transform.position = posList[end].position;

        //     Vector3 finalDirection = posList[end].position - origin.position;
        //     arrowhead.transform.rotation = Quaternion.LookRotation(finalDirection);
        //     arrowhead.transform.Rotate(-90, 0, 0);
        // }
    }
}
