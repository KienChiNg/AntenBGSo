using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAnten : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private Anten antenHt;
    [SerializeField] private Anten antenHr;
    [SerializeField] private TMP_Text htValueTxt;
    [SerializeField] private TMP_Text hrValueTxt;
    [SerializeField] private Slider sliderHt;
    [SerializeField] private Slider sliderHr;
    [SerializeField] private TMP_Text r0Value;
    [SerializeField] private GameObject r0TextGO;
    private double valueHt, valueHr, valueR0;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        sliderHt.onValueChanged.AddListener(OnSliderHtValueChanged);
        sliderHr.onValueChanged.AddListener(OnSliderHrValueChanged);
    }
    private void Start()
    {
        SetInitLineRenderer();

        sliderHt.value = 200;
        sliderHr.value = 200;
        OnSliderHtValueChanged(200);
        OnSliderHrValueChanged(200);
        // CalValueR0();
    }
    private void OnSliderHtValueChanged(float arg0)
    {
        antenHt.SetHeightAnten(arg0);
        valueHt = arg0;
        CalValueR0();
    }

    private void OnSliderHrValueChanged(float arg0)
    {
        antenHr.SetHeightAnten(arg0);
        valueHr = arg0;
        CalValueR0();
    }
    private void CalValueR0()
    {
        valueR0 = 3570f * (Math.Sqrt(valueHt) + Math.Sqrt(valueHr));
        SetValueR0(FormatString(valueR0 * Math.Pow(10, -3)));
        CalAngleTwoAnten();
        SetContentValueAnten();
        lineRenderer.SetPosition(0, antenHt.GetTopAnten());
        lineRenderer.SetPosition(1, antenHr.GetTopAnten());
    }
    private void SetContentValueAnten()
    {
        htValueTxt.text = FormatString(valueHt) + " m";
        hrValueTxt.text = FormatString(valueHr) + " m";
    }
    private void CalAngleTwoAnten()
    {
        double a = valueR0;
        double b = valueHt + 6370000;
        double c = valueHr + 6370000;
        double angle = Math.Acos((b * b + c * c - a * a) / (2 * b * c)) * 38 * 180 / Math.PI;
        antenHt.CalPosAnten((float)angle / 2);
        antenHr.CalPosAnten(-(float)angle / 2);
        CalMidpoint();
    }
    private void SetValueR0(string content)
    {
        r0Value.text = $"r<sub>0</sub> =  {content} (km)";
    }

    void SetInitLineRenderer()
    {
        lineRenderer.widthMultiplier = 2f;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.textureMode = LineTextureMode.Tile;
    }
    // Update is called once per frame
    void Update()
    {
        // lineRenderer.SetPosition(0, antenHt.GetTopAnten());
        // lineRenderer.SetPosition(1, antenHr.GetTopAnten());
    }

    void CalMidpoint()
    {
        Vector3 midPoint = (lineRenderer.GetPosition(0) + lineRenderer.GetPosition(1)) / 2;
        // Debug.Log(antenHt.GetTopAnten());
        r0TextGO.transform.position = new Vector3(midPoint.x, midPoint.y + 0.1f, midPoint.z);
    }

    void OnDestroy()
    {
        // Hủy đăng ký sự kiện onValueChanged khi Script bị hủy
        if (antenHt != null)
        {
            sliderHt.onValueChanged.AddListener(OnSliderHtValueChanged);
        }
        if (antenHr != null)
        {
            sliderHr.onValueChanged.AddListener(OnSliderHrValueChanged);
        }
    }
    string FormatString(double value)
    {
        value = Math.Round(value, 2); // làm tròn giá trị đến 2 chữ số thập phân

        if (value % 1 != 0) // Kiểm tra là số thực
            return value.ToString("0.00");
        else
            return value.ToString("0");
    }
}
