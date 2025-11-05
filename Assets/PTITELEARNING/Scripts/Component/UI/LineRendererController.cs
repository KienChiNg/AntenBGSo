using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class LineRendererController : MonoBehaviour
{
    public float speed = 1f;
    float width, height;
    public int currentValue = 0;
    UILineRenderer line;
    
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<UILineRenderer>();
        //
        Rect r = GetComponent<RectTransform>().rect;
        width = r.width;
        height = r.height;
        //
        InitLine();
    }

    private void InitLine()
    {
        List<Vector2> points = new List<Vector2>();
        for(int i = 0; i < 12; i ++)
        { 
            points.Add(new Vector2(i * 0.1f * width - width/2, GetValue()));
        }

        line.Points = points.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToLeft();
    }

    private void MoveToLeft()
    {
        List<Vector2> points = new List<Vector2>(line.Points);
        //
        for(int i = 0; i < points.Count; i ++)
        {
            points[i] = new Vector2(points[i].x - Time.deltaTime * speed, points[i].y); 
        }
        //
        if(points[1].x < -width/2)
        {
            points.RemoveAt(0);
            //add new
            points.Add(new Vector2(1.1f * width - width/2, GetValue()));
        }

        line.Points = points.ToArray();
    }

    public float GetValue()
    {
        if(currentValue == 0)
        {
            return -0.4f * height;
        } else
        {
            return 0.4f * height;
        }
    }
}
