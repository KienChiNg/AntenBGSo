using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWave : MonoBehaviour
{
    public enum TypePolarization : byte
    {
        Horizontal,
        Vertical,
        LeftCircular,
        RightCircular
    }

    [HideInInspector]
    public bool wave1;
    [HideInInspector]
    public TypePolarization polarization;
    [HideInInspector]
    public int polarizationIndex;
    [Range(0, 10)]
    [HideInInspector]
    public int ampli1;
    [Range(1, 8)]
    [HideInInspector]
    public int wwll1;
    [HideInInspector]
    public bool rev;

    [HideInInspector]
    public bool wave2;
    [HideInInspector]
    public TypePolarization polarization2;
    [HideInInspector]
    public int polarizationIndex2;

    [Range(0, 10)]
    [HideInInspector]
    public int ampli2;
    [Range(1, 8)]
    [HideInInspector]
    public int wwll2;
    [Range(-18, 18)]
    [HideInInspector]
    public int phase = 0;

    Vector3 vw1, vw2, vSum;
    double wale1, freq1, amp1, wale2, freq2, amp2, pdif, e1, n1, xd1a, xd1b, e2, n2, xd2a, xd2b;
    double t = 0.0f;
    double step = Math.PI / 18;
    double pih = Math.PI / 2;
    List<double> x = new List<double>();
    List<double> xbefore = new List<double>();
    List<double> xbetween = new List<double>();
    List<double> xafter = new List<double>();
    List<GameObject> dotLst = new List<GameObject>();
    List<GameObject> dotLst2 = new List<GameObject>();
    List<GameObject> dotLstSum = new List<GameObject>();
    [HideInInspector]
    public bool wave1AddWave2;
    [HideInInspector]
    public bool cMatOn;

    [Range(1, 32)]
    [HideInInspector]
    public int cmatlen;
    [Range(0, 20)]
    [HideInInspector]
    public int w1e;
    [Range(0, 20)]
    [HideInInspector]
    public int w1n;

    [Range(0, 20)]
    [HideInInspector]
    public int w2e;
    [Range(0, 20)]
    [HideInInspector]
    public int w2n;

    [HideInInspector]
    public bool hideComponents;
    [HideInInspector]
    public bool hideSuperposition;
    [HideInInspector]
    public float speed;
    int ibefore, iafter;
    public List<GameObject> drawAreas;
    public GameObject dot;
    public GameObject materialGO;
    public ColorOptions colorOptions;
    public List<GameObject> arrows;
    public List<LineRenderer> lineRendererHelperLines;
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();
    private void Awake()
    {
        for (int i = 0; i < drawAreas.Count; i++)
        {
            lineRenderers.Add(drawAreas[i].GetComponent<LineRenderer>());
        }

    }
    private void Start()
    {
        colorOptions.SetColorResetMode();
    }
    public void Init()
    {
        //? Tính để 3 mảng dotLst, dotLs2, dotLstSum chỉ là mảng Vector3 nhưng lười đổi sau khi test xong =))
        for (double c = -8 * Math.PI; c < 8 * Math.PI + step / 2; c += step)
        {
            x.Add(c);
            GameObject gameObject = Instantiate(dot, drawAreas[0].transform);
            gameObject.GetComponent<SpriteRenderer>().color = Const.ORIGINAL_COLOR_WAVE_1;
            gameObject.SetActive(false);

            GameObject gameObject2 = Instantiate(dot, drawAreas[1].transform);
            gameObject2.GetComponent<SpriteRenderer>().color = Const.ORIGINAL_COLOR_WAVE_2;
            gameObject2.SetActive(false);

            GameObject gameObject3 = Instantiate(dot, drawAreas[2].transform);
            gameObject3.GetComponent<SpriteRenderer>().color = Const.ORIGINAL_COLOR_WAVE_1_2;
            gameObject3.SetActive(false);

            dotLst.Add(gameObject);
            dotLst2.Add(gameObject2);
            dotLstSum.Add(gameObject3);

            lineRenderers[0].positionCount = dotLst.Count;
            lineRenderers[1].positionCount = dotLst2.Count;
            lineRenderers[2].positionCount = dotLstSum.Count;
        }

        foreach (LineRenderer lineRenderer in lineRenderers)
        {
            lineRenderer.startWidth = 0.2f;
            lineRenderer.endWidth = 0.2f;
        }

        lineRendererHelperLines[0].positionCount = 3;
        lineRendererHelperLines[1].positionCount = 3;
        foreach (LineRenderer lineRenderer in lineRendererHelperLines)
        {
            lineRenderer.startWidth = 0.2f;
            lineRenderer.endWidth = 0.2f;
        }
        // Debug.Log(-8 * Math.PI);
        // GameObject squareGO = Instantiate(square,drawAreas[0].transform.parent);
        // squareGO.transform.position = new Vector3((float)(-8 * Math.PI),0,0);
        // GameObject squareGO2 = Instantiate(square,drawAreas[0].transform.parent);
        // squareGO2.transform.position = new Vector3((float)(8 * Math.PI),0,0);
    }
    // Update is called once per frame
    void Update()
    {
        Spin();
    }
    void Spin()
    {
        wale1 = wwll1 / 4.0;
        freq1 = 1.0 / wale1;
        amp1 = ampli1;

        wale2 = wwll2 / 4.0;
        freq2 = 1.0 / wale2;
        amp2 = ampli2;
        pdif = phase * 10 / 180.0 * MathF.PI;

        ibefore = Mathf.FloorToInt((float)(144 - 4.5 * cmatlen));
        iafter = Mathf.CeilToInt((float)(144 + 4.5 * cmatlen));

        xbefore = Slice(x, 0, ibefore);
        xbetween = Slice(x, ibefore, iafter);
        xafter = Slice(x, iafter, x.Count);

        polarization = SetPolarization(polarizationIndex);
        polarization2 = SetPolarization(polarizationIndex2);

        if (cMatOn)
        {
            materialGO.transform.localScale = new Vector3((float)cmatlen / 32, 1, 1);
            materialGO.SetActive(true);
            e1 = w1e / 20.0f;
            n1 = 1.0 + w1n / 20.0f;
            xd1a = xbetween[0] / wale1 * (1 - n1);
            xd1b = xafter[0] / wale1 * (n1 - 1) + xd1a;

            e2 = w2e / 20.0f;
            n2 = 1.0 + w2n / 20.0f;
            xd2a = xbetween[0] / wale2 * (1 - n2);
            xd2b = xafter[0] / wale2 * (n2 - 1) + xd2a;
        }
        else
            materialGO.SetActive(false);

        for (int i = 0; i < x.Count; i++)
        {
            if (cMatOn)
                vw1 = Formula4(x[i]);
            else
                vw1 = Formula(x[i]);
            dotLst[i].transform.position = vw1;
            drawAreas[0].SetActive(wave1);
            lineRenderers[0].SetPosition(i, dotLst[i].transform.position);

            if (cMatOn)
                vw2 = Formula5(x[i]);
            else
                vw2 = Formula2(x[i]);
            dotLst2[i].transform.position = vw2;
            drawAreas[1].SetActive(wave2);
            lineRenderers[1].SetPosition(i, dotLst2[i].transform.position);

            vSum = Formula3(vw1, vw2);
            dotLstSum[i].transform.position = vSum;
            drawAreas[2].SetActive(wave1AddWave2);
            lineRenderers[2].SetPosition(i, dotLstSum[i].transform.position);
        }

        if (hideComponents)
        {
            drawAreas[0].SetActive(false);
            drawAreas[1].SetActive(false);
        }

        if (hideSuperposition)
            drawAreas[2].SetActive(false);

        if (wave1)
        {
            SetUpArrow(new Vector3((float)(-8 * Math.PI), 0, 0), new Vector3((float)(-8 * Math.PI), 2, 0), dotLst[0], 0, 0);
            SetUpArrow(new Vector3((float)(8 * Math.PI), 0, 0), new Vector3((float)(8 * Math.PI), 2, 0), dotLst[dotLst.Count - 1], 0, 1);
        }
        arrows[0].SetActive(wave1);
        if (wave2)
        {
            SetUpArrow(new Vector3((float)(-8 * Math.PI), 0, 0), new Vector3((float)(-8 * Math.PI), 2, 0), dotLst2[0], 1, 0);
            SetUpArrow(new Vector3((float)(8 * Math.PI), 0, 0), new Vector3((float)(8 * Math.PI), 2, 0), dotLst2[dotLst2.Count - 1], 1, 1);
        }
        arrows[1].SetActive(wave2);
        if (wave1AddWave2)
        {
            SetUpArrow(new Vector3((float)(-8 * Math.PI), 0, 0), new Vector3((float)(-8 * Math.PI), 2, 0), dotLstSum[0], 2, 0);
            SetUpArrow(new Vector3((float)(8 * Math.PI), 0, 0), new Vector3((float)(8 * Math.PI), 2, 0), dotLstSum[dotLstSum.Count - 1], 2, 1);
        }
        arrows[2].SetActive(wave1AddWave2);

        lineRendererHelperLines[0].gameObject.SetActive(wave1AddWave2);
        lineRendererHelperLines[1].gameObject.SetActive(wave1AddWave2);
        if (wave1AddWave2)
        {
            Vector3 startPoint = dotLst[0].transform.position;
            Vector3 midPoint = dotLstSum[0].transform.position;
            Vector3 endPoint = dotLst2[0].transform.position;
            lineRendererHelperLines[0].SetPosition(0, startPoint);
            lineRendererHelperLines[0].SetPosition(1, midPoint);
            lineRendererHelperLines[0].SetPosition(2, endPoint);

            Vector3 startPointFace = dotLst[dotLst.Count - 1].transform.position;
            Vector3 midPointFace = dotLstSum[dotLstSum.Count - 1].transform.position;
            Vector3 endPointFace = dotLst2[dotLst.Count - 1].transform.position;
            lineRendererHelperLines[1].SetPosition(0, startPointFace);
            lineRendererHelperLines[1].SetPosition(1, midPointFace);
            lineRendererHelperLines[1].SetPosition(2, endPointFace);
        }

        t += step / 50 * speed;
    }

    private void SetUpArrow(Vector3 startPoint, Vector3 endPoint, GameObject posTarget, int inxArr, int inxArrChild)
    {
        Vector3 originalScale = new Vector3(30f, 60f, 60f);
        arrows[inxArr].transform.GetChild(inxArrChild).position = startPoint;
        Vector3 direction1 = (endPoint - startPoint).normalized;
        Vector3 direction2 = (posTarget.transform.position - startPoint).normalized;

        // Tính góc giữa hai đường thẳng (theo đơn vị độ)
        float angle = Vector3.Angle(direction1, direction2);
        if ((direction1 - direction2).z > 0) angle *= -1;
        Quaternion rotation = Quaternion.Euler(angle, 0f, 90);
        arrows[inxArr].transform.GetChild(inxArrChild).rotation = rotation;


        Vector3 direction = posTarget.transform.position - startPoint;
        float length = direction.magnitude;
        if (length > 0)
            arrows[inxArr].transform.GetChild(inxArrChild).localScale = new Vector3(
                length * 26f,
                arrows[inxArr].transform.GetChild(inxArrChild).localScale.y,
                arrows[inxArr].transform.GetChild(inxArrChild).localScale.z);

        arrows[inxArr].transform.GetChild(inxArrChild).GetChild(0).localScale = new Vector3(
                    originalScale.x / arrows[inxArr].transform.GetChild(inxArrChild).localScale.x,
                    originalScale.y / arrows[inxArr].transform.GetChild(inxArrChild).localScale.y,
                    originalScale.z / arrows[inxArr].transform.GetChild(inxArrChild).localScale.z);
    }

    TypePolarization SetPolarization(int inx)
    {
        switch (inx)
        {
            case 0:
                return TypePolarization.Horizontal;
            case 1:
                return TypePolarization.Vertical;
            case 2:
                return TypePolarization.LeftCircular;
            case 3:
                return TypePolarization.RightCircular;
            default:
                return TypePolarization.Horizontal;
        }
    }

    Vector3 Formula(double x)
    {
        Vector3 v3 = Vector3.zero;
        double vX = x, vY = 0, vZ = 0;
        int j = rev ? -1 : 1;
        x *= j;
        switch (polarization)
        {
            case TypePolarization.Horizontal:
                vZ = amp1 * Math.Sin(x / wale1 - freq1 * t);
                vY = 0.0;
                break;
            case TypePolarization.Vertical:
                vY = amp1 * Math.Sin(x / wale1 - freq1 * t);
                vZ = 0.0;
                break;
            case TypePolarization.LeftCircular:
                vZ = amp1 * Math.Sin(x / wale1 - freq1 * t);
                vY = amp1 * Math.Sin(x / wale1 - freq1 * t - pih);
                break;
            case TypePolarization.RightCircular:
                vZ = amp1 * Math.Sin(x / wale1 - freq1 * t);
                vY = amp1 * Math.Sin(x / wale1 - freq1 * t + pih);
                break;
        }
        v3 = new Vector3((float)vX, (float)vY, (float)vZ);
        return v3;
    }
    Vector3 Formula2(double x)
    {
        Vector3 v3 = Vector3.zero;
        double vX = x, vY = 0, vZ = 0;
        switch (polarization2)
        {
            case TypePolarization.Horizontal:
                vZ = amp2 * Math.Sin(x / wale2 - freq2 * t + pdif);
                vY = 0.0;
                break;
            case TypePolarization.Vertical:
                vY = amp2 * Math.Sin(x / wale2 - freq2 * t + pdif);
                vZ = 0.0;
                break;
            case TypePolarization.LeftCircular:
                vZ = amp2 * Math.Sin(x / wale2 - freq2 * t + pdif);
                vY = amp2 * Math.Sin(x / wale2 - freq2 * t + pdif - pih);
                break;
            case TypePolarization.RightCircular:
                vZ = amp2 * Math.Sin(x / wale2 - freq2 * t + pdif);
                vY = amp2 * Math.Sin(x / wale2 - freq2 * t + pdif + pih);
                break;
        }
        v3 = new Vector3((float)vX, (float)vY, (float)vZ);
        return v3;
    }
    Vector3 Formula3(Vector3 p1, Vector3 p2)
    {
        Vector3 v3 = Vector3.zero;
        double vY = 0, vZ = 0;
        vZ = p1.z + p2.z;
        vY = p1.y + p2.y;
        v3 = new Vector3(p2.x, (float)vY, (float)vZ);
        return v3;
    }
    Vector3 Formula4(double x)
    {
        Vector3 v3 = Vector3.zero;
        double vX = x, vY = 0, vZ = 0;
        int j = rev ? -1 : 1;
        x *= j;
        switch (polarization)
        {
            case TypePolarization.Horizontal:
                if (x < xbetween[0])
                {
                    vZ = amp1 * Math.Sin(x / wale1 - freq1 * t);
                }
                else if (x < xafter[0])
                {
                    vZ = amp1 * Math.Exp(e1 * (xbetween[0] - x) / Math.PI) * Math.Sin(x / wale1 * n1 - freq1 * t + xd1a);
                }
                else
                {
                    vZ = amp1 * Math.Exp(e1 * (xbetween[0] - xafter[0]) / Math.PI) * Math.Sin(x / wale1 - freq1 * t + xd1b);
                }
                vY = 0.0;
                break;
            case TypePolarization.Vertical:
                if (x < xbetween[0])
                {
                    vY = amp1 * Math.Sin(x / wale1 - freq1 * t);
                }
                else if (x < xafter[0])
                {
                    vY = amp1 * Math.Exp(e1 * (xbetween[0] - x) / Math.PI) * Math.Sin(x / wale1 * n1 - freq1 * t + xd1a);
                }
                else
                {
                    vY = amp1 * Math.Exp(e1 * (xbetween[0] - xafter[0]) / Math.PI) * Math.Sin(x / wale1 - freq1 * t + xd1b);
                }
                vZ = 0.0;
                break;
            case TypePolarization.LeftCircular:
                if (x < xbetween[0])
                {
                    vZ = amp1 * Math.Sin(x / wale1 - freq1 * t);
                    vY = amp1 * Math.Sin(x / wale1 - freq1 * t - pih);
                }
                else if (x < xafter[0])
                {
                    vZ = amp1 * Math.Exp(e1 * (xbetween[0] - x) / Math.PI) * Math.Sin(x / wale1 * n1 - freq1 * t + xd1a);
                    vY = amp1 * Math.Exp(e1 * (xbetween[0] - x) / Math.PI) * Math.Sin(x / wale1 * n1 - freq1 * t + xd1a - pih);
                }
                else
                {
                    vZ = amp1 * Math.Exp(e1 * (xbetween[0] - xafter[0]) / Math.PI) * Math.Sin(x / wale1 - freq1 * t + xd1b);
                    vY = amp1 * Math.Exp(e1 * (xbetween[0] - xafter[0]) / Math.PI) * Math.Sin(x / wale1 - freq1 * t + xd1b - pih);
                }
                break;
            case TypePolarization.RightCircular:
                if (x < xbetween[0])
                {
                    vZ = amp1 * Math.Sin(x / wale1 - freq1 * t);
                    vY = amp1 * Math.Sin(x / wale1 - freq1 * t + pih);
                }
                else if (x < xafter[0])
                {
                    vZ = amp1 * Math.Exp(e1 * (xbetween[0] - x) / Math.PI) * Math.Sin(x / wale1 * n1 - freq1 * t + xd1a);
                    vY = amp1 * Math.Exp(e1 * (xbetween[0] - x) / Math.PI) * Math.Sin(x / wale1 * n1 - freq1 * t + xd1a + pih);
                }
                else
                {
                    vZ = amp1 * Math.Exp(e1 * (xbetween[0] - xafter[0]) / Math.PI) * Math.Sin(x / wale1 - freq1 * t + xd1b);
                    vY = amp1 * Math.Exp(e1 * (xbetween[0] - xafter[0]) / Math.PI) * Math.Sin(x / wale1 - freq1 * t + xd1b + pih);
                }
                break;
        }
        v3 = new Vector3((float)vX, (float)vY, (float)vZ);
        return v3;
    }
    Vector3 Formula5(double x)
    {
        Vector3 v3 = Vector3.zero;
        double vX = x, vY = 0, vZ = 0;
        switch (polarization2)
        {
            case TypePolarization.Horizontal:
                if (x < xbetween[0])
                {
                    vZ = amp2 * Math.Sin(x / wale2 - freq2 * t + pdif);
                }
                else if (x < xafter[0])
                {
                    vZ = amp2 * Math.Exp(e2 * (xbetween[0] - x) / Math.PI) * Math.Sin(x / wale2 * n2 - freq2 * t + pdif + xd2a);
                }
                else
                {
                    vZ = amp2 * Math.Exp(e2 * (xbetween[0] - xafter[0]) / Math.PI) * Math.Sin(x / wale2 - freq2 * t + pdif + xd2b);
                }
                vY = 0.0;
                break;
            case TypePolarization.Vertical:
                if (x < xbetween[0])
                {
                    vY = amp2 * Math.Sin(x / wale2 - freq2 * t + pdif);
                }
                else if (x < xafter[0])
                {
                    vY = amp2 * Math.Exp(e2 * (xbetween[0] - x) / Math.PI) * Math.Sin(x / wale2 * n2 - freq2 * t + pdif + xd2a);
                }
                else
                {
                    vY = amp2 * Math.Exp(e2 * (xbetween[0] - xafter[0]) / Math.PI) * Math.Sin(x / wale2 - freq2 * t + pdif + xd2b);
                }
                vZ = 0.0;
                break;
            case TypePolarization.LeftCircular:
                if (x < xbetween[0])
                {
                    vZ = amp2 * Math.Sin(x / wale2 - freq2 * t + pdif);
                    vY = amp2 * Math.Sin(x / wale2 - freq2 * t + pdif - pih);
                }
                else if (x < xafter[0])
                {
                    vZ = amp2 * Math.Exp(e2 * (xbetween[0] - x) / Math.PI) * Math.Sin(x / wale2 * n2 - freq2 * t + pdif + xd2a);
                    vY = amp2 * Math.Exp(e2 * (xbetween[0] - x) / Math.PI) * Math.Sin(x / wale2 * n2 - freq2 * t + pdif + xd2a - pih);
                }
                else
                {
                    vZ = amp2 * Math.Exp(e2 * (xbetween[0] - xafter[0]) / Math.PI) * Math.Sin(x / wale2 - freq2 * t + pdif + xd2b);
                    vY = amp2 * Math.Exp(e2 * (xbetween[0] - xafter[0]) / Math.PI) * Math.Sin(x / wale2 - freq2 * t + pdif + xd2b - pih);
                }
                break;
            case TypePolarization.RightCircular:
                if (x < xbetween[0])
                {
                    vZ = amp2 * Math.Sin(x / wale2 - freq2 * t + pdif);
                    vY = amp2 * Math.Sin(x / wale2 - freq2 * t + pdif + pih);
                }
                else if (x < xafter[0])
                {
                    vZ = amp2 * Math.Exp(e2 * (xbetween[0] - x) / Math.PI) * Math.Sin(x / wale2 * n2 - freq2 * t + pdif + xd2a);
                    vY = amp2 * Math.Exp(e2 * (xbetween[0] - x) / Math.PI) * Math.Sin(x / wale2 * n2 - freq2 * t + pdif + xd2a + pih);
                }
                else
                {
                    vZ = amp2 * Math.Exp(e2 * (xbetween[0] - xafter[0]) / Math.PI) * Math.Sin(x / wale2 - freq2 * t + pdif + xd2b);
                    vY = amp2 * Math.Exp(e2 * (xbetween[0] - xafter[0]) / Math.PI) * Math.Sin(x / wale2 - freq2 * t + pdif + xd2b + pih);
                }
                break;
        }
        v3 = new Vector3((float)vX, (float)vY, (float)vZ);
        return v3;
    }
    List<double> Slice(List<double> xLst, int inxStart, int inxEnd)
    {
        List<double> sliceLst = new List<double>();
        for (int i = inxStart; i < inxEnd; i++)
        {
            sliceLst.Add(xLst[i]);
        }
        return sliceLst;
    }
}
