using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;

public class Chart : MonoBehaviour
{
    public LineRenderer lineRendererX;
    public LineRenderer lineRendererY;
    public List<LineRenderer> lineRendererXLabelList;
    public List<LineRenderer> lineRendererYLabelList;
    public LineRenderer lineRendererChart;
    public GameObject arrowX;
    public GameObject arrowY;
    public GameObject car;
    public GameObject lableText;
    public GameObject lableFText;
    public GameObject lableAText;
    public Transform canvas;
    public CalDoppler calDoppler;
    private List<GameObject> labelXList = new List<GameObject>();
    private List<GameObject> labelYList = new List<GameObject>();
    private float xValue = 18;
    private float yValue = 7;
    private void Start()
    {
        calDoppler.OnChartChange += OnChartChange;
        DrawAxis();
    }

    private void DrawAxis()
    {
        SetLineRenderderInit(lineRendererX, 2);
        SetLineRenderderInit(lineRendererY, 2);
        SetLineRenderderInit(lineRendererChart, 181);

        for (int i = 0; i < lineRendererXLabelList.Count; i++)
        {
            SetLineRenderderInit(lineRendererXLabelList[i], 2, 0.1f);
        }

        for (int i = 0; i < lineRendererYLabelList.Count; i++)
        {
            SetLineRenderderInit(lineRendererYLabelList[i], 2, 0.1f);
        }

        DrawInit();
    }

    private void SetLineRenderderInit(LineRenderer lineRenderer, int count, float width = 0.12f)
    {
        lineRenderer.positionCount = count;
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }
    private void DrawInit()
    {
        lineRendererX.SetPosition(0, new Vector3(0, 0, 0));
        lineRendererX.SetPosition(1, new Vector3(xValue + 1, 0, 0));
        arrowX.transform.localPosition = new Vector3(xValue + 1, 0, 0);

        lineRendererY.SetPosition(0, new Vector3(0, 0, 0));
        lineRendererY.SetPosition(1, new Vector3(0, yValue + 1, 0));
        arrowY.transform.localPosition = new Vector3(0, yValue + 1, 0);

        GameObject label = new GameObject();
        for (int i = 0; i < lineRendererXLabelList.Count; i++)
        {
            lineRendererXLabelList[i].SetPosition(0, new Vector3(xValue / (lineRendererXLabelList.Count - 1) * i, -0.2f, 0));
            lineRendererXLabelList[i].SetPosition(1, new Vector3(xValue / (lineRendererXLabelList.Count - 1) * i, 0.2f, 0));

            label = Instantiate(lableText, canvas);
            label.SetActive(true);
            label.transform.localPosition = new Vector3(lineRendererXLabelList[i].GetPosition(0).x, lineRendererXLabelList[i].GetPosition(0).y - 0.6f, 0);
            labelXList.Add(label);
        }

        for (int i = 0; i < lineRendererYLabelList.Count; i++)
        {
            lineRendererYLabelList[i].SetPosition(0, new Vector3(-0.2f, yValue / (lineRendererYLabelList.Count - 1) * i, 0));
            lineRendererYLabelList[i].SetPosition(1, new Vector3(0.2f, yValue / (lineRendererYLabelList.Count - 1) * i, 0));

            label = Instantiate(lableText, canvas);
            label.SetActive(true);
            label.transform.localPosition = new Vector3(lineRendererYLabelList[i].GetPosition(0).x - 1f, lineRendererYLabelList[i].GetPosition(0).y, 0);
            labelYList.Add(label);
        }

        lableAText.transform.localPosition = new Vector3(lineRendererXLabelList[lineRendererXLabelList.Count - 1].GetPosition(0).x + 1, lineRendererXLabelList[lineRendererXLabelList.Count - 1].GetPosition(0).y + 1.2f, 0);
        lableFText.transform.localPosition = new Vector3(lineRendererYLabelList[lineRendererYLabelList.Count - 1].GetPosition(0).x + 1f, lineRendererYLabelList[lineRendererYLabelList.Count - 1].GetPosition(0).y + 1, 0);
    }

    private void OnChartChange(float obj, int angle)
    {
        Vector3 v3 = new Vector3();
        float valYAxis = 0;
        float valXAxis = 0;
        int chartCount = lineRendererChart.positionCount;
        for (int i = 0; i < chartCount; i++)
        {
            valXAxis = xValue / chartCount * i;
            valYAxis = obj != 0 ? (yValue * (float)(obj * Math.Cos(i * Math.PI / 180)) / (float)(2 * obj * Math.Cos(2 * Math.PI))) : 0;
            v3 = new Vector3(valXAxis, valYAxis + yValue / 2, 0);
            lineRendererChart.SetPosition(i, v3);
        }

        for (int i = 0; i < labelXList.Count; i++)
        {
            valXAxis = (chartCount - 1) / (labelXList.Count - 1) * i;
            labelXList[i].GetComponent<TMP_Text>().text = ConvertNum(valXAxis);
        }

        // Debug.Log((2 * obj * Math.Cos(2 * Math.PI)));
        float maxValue = obj != 0 ? (float)(obj * Math.Cos(2 * Math.PI)) : 1;
        for (int i = 0; i < labelYList.Count; i++)
        {
            valYAxis = 2 * maxValue / (labelYList.Count - 1) * i - maxValue;
            labelYList[i].GetComponent<TMP_Text>().text = ConvertNum(valYAxis);
        }
        car.transform.localPosition = new Vector3(lineRendererChart.GetPosition(angle).x, lineRendererChart.GetPosition(angle).y, -0.2f);
    }
    private string ConvertNum(float val)
    {
        if (IsInteger(val))
        {
            return val.ToString("0");
        }
        return val.ToString("0.0");
    }
    bool IsInteger(float value)
    {
        return value % 1 == 0;
    }
}
