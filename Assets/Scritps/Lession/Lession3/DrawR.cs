using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawR : MonoBehaviour
{
    private float dH_dt = -0.14f;
    [SerializeField] private GameObject dot;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject displayNormalOrbit;
    [SerializeField] private GameObject displayEsOrbit;
    [SerializeField] private TMP_Text displaydH_dtTxt;
    [SerializeField] private TMP_Text displayRTxt;
    [SerializeField] private TMP_Text displayAtdTxt;
    private float dH_dtDisplay;
    private float atd;
    private float R;
    private float RDisplay;
    private float a = 6370000;
    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        OnSliderValueChanged(0);
    }
    private void Update()
    {
    }
    private void OnSliderValueChanged(float arg0)
    {
        dH_dtDisplay = arg0 * 10;
        dH_dt = arg0 - 0.14f;
        // Debug.Log(dH_dt);
        if (dH_dtDisplay < -0.157)
        {
            displayNormalOrbit.SetActive(false);
            displayEsOrbit.SetActive(true);
        }
        else
        {
            displayNormalOrbit.SetActive(true);
            displayEsOrbit.SetActive(false);
        }

        SetValuedH_dtText();
        CalAtd();
        CalR();
    }
    private void CalAtd()
    {
        float m = 1 + a * dH_dtDisplay * Mathf.Pow(10, -6);
        if (m != 0)
        {
            atd = a / m;
        }
        SetValueAtdText();
        // dot.transform.position = new Vector3(0, atd * 0.5f / a, 0);
    }
    private void CalRDisplay()
    {
        if (dH_dt != 0)
        {
            R = Mathf.Pow(10, 6) / -dH_dt;
        }
        float y = R / a * 0.57f;
        dot.transform.localPosition = new Vector3(0, y, 0);
    }
    private void CalR()
    {
        // Debug.Log(dH_dtDisplay.ToString("F0"));
        if (dH_dtDisplay != 0)
        {
            RDisplay = Mathf.Pow(10, 6) / -dH_dtDisplay;
        }
        else
        {
            RDisplay = 0;
        }
        SetValueRText();
        CalRDisplay();
    }
    private void SetValuedH_dtText()
    {
        displaydH_dtTxt.text = FormatString(dH_dtDisplay);
    }
    private void SetValueRText()
    {
        displayRTxt.text = $"R = {FormatStringPow10(RDisplay)}";
    }
    private void SetValueAtdText()
    {
        displayAtdTxt.text = $"a<sub>td</sub> = {FormatStringPow10(atd)}";
    }
    string FormatString(float value)
    {
        value = (float)Math.Round(value, 3);

        if (value % 1 != 0) // Kiểm tra là số thực
            return value.ToString("0.000");
        else
            return value.ToString("0");
    }
    string FormatStringPow10(float value)
    {
        if (value < 10 && value > 0) return FormatString(value);
        else if (value == 0) return "∞";
        else if (value < 0) return "< 0";
        int t = 0;
        while (value > 10)
        {
            value /= 10;
            t++;
        }

        if (t >= 10) return "∞";
        else
        {
            if (value != 1)
                return FormatString(value) + ".10" + "<sup>" + t + "</sup>";
            else
                return "10" + "<sup>" + t + "</sup>";
        }
    }
}
