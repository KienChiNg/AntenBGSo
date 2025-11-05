using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalDoppler : MonoBehaviour
{
    [SerializeField] private Camera cameraTexRender;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform carPos;
    [SerializeField] private Transform sourceS;
    [SerializeField] private Transform anten;
    [SerializeField] private Transform arrowHead;
    [SerializeField] private Slider sliderFo;
    [SerializeField] private Slider sliderV;
    [SerializeField] private Slider posCar;
    [SerializeField] private TMP_Text foValueTxt;
    [SerializeField] private TMP_Text vValueTxt;
    [SerializeField] private TMP_Text alphaTxt;
    [SerializeField] private TMP_Text fTxt;
    [SerializeField] private TMP_Text fRTxt;
    [SerializeField] private TMP_Text lamdaTxt;
    [SerializeField] private Toggle toggleAutoCar;
    [SerializeField] private GameObject car;
    [SerializeField] private GameObject alphaSymbol;
    [SerializeField] private GameObject chart;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LineRenderer lineRendererAlpha;
    [SerializeField] private LineRenderer lineRendererSinWave;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Material materialTransparent;
    [SerializeField] private Material materialNormal;
    [SerializeField] private List<SelectSlider> selectSliders;

    private double c = 3 * Math.Pow(10, 8);
    private float foValue = 300;
    private float vValue = 0;
    private float lamdaValue = 0;
    private double alphaAngle;
    private float camPosZInit;
    private float sourceSPosYInit;
    private float antenScaleInit;
    private float carPosYInit;
    private double fDopplerValue;
    private Vector3 posSToCar;
    private Vector3 carToArrowHead;
    private bool isToggleAutoCar;
    private float directionToTarget;
    public Action<float, int> OnChartChange;
    // private float rotationAngle = 45.0f;
    private List<MeshRenderer> transparentObjects = new List<MeshRenderer>();
    private void Awake()
    {
        // lineRenderer = GetComponent<LineRenderer>();
    }
    void Start()
    {
        directionToTarget = Vector3.Distance(car.transform.position, cam.position);

        sliderFo.onValueChanged.AddListener(OnSliderFoValueChanged);
        sliderV.onValueChanged.AddListener(OnSliderVValueChanged);
        posCar.onValueChanged.AddListener(OnSliderPosCarChanged);
        toggleAutoCar.onValueChanged.AddListener(OnToggleAutoCarChanged);

        foreach (SelectSlider selectSlider in selectSliders)
        {
            selectSlider.onSelectInx += SetHighlight;
        }

        camPosZInit = Camera.main.transform.position.z;
        sourceSPosYInit = sourceS.position.y;
        carPosYInit = carPos.position.y;
        antenScaleInit = anten.localScale.y;

        SetInitLineRenderer();
        SetFoValueText();
        SetVValueText();
        fDoppler();
        OnSliderPosCarChanged(0);

    }
    void OnEnable()
    {
        if (posCar != null)
            posCar.value = 0;
        chart.SetActive(true);
        if (cameraTexRender != null)
        {
            cameraTexRender.transform.position = new Vector3(0, 22.67469f, -40.33629f);
            cameraTexRender.transform.rotation = Quaternion.Euler(17.287f, 0, 0);
        }
    }
    void OnDisable()
    {
        if (cameraTexRender != null)
        {
            cameraTexRender.transform.position = new Vector3(0, 22.67469f, -40.33629f);
            cameraTexRender.transform.rotation = Quaternion.Euler(17.287f, 0, 0);
        }
        if (chart != null)
            chart.SetActive(false);
    }
    void SetHighlight(bool obj, int inx)
    {
        // Debug.Log(inx + " " + obj);
        Color color = !obj ? new Color32(122, 122, 122, 255) : Color.red;
        // if(toggleAutoCar) color = Color.red;
        switch (inx)
        {
            case 0:
                alphaTxt.color = color;
                if (vValue != 0)
                    fTxt.color = color;
                break;
            case 1:
                lamdaTxt.color = color;
                fRTxt.color = color;
                break;
            case 2:
                fRTxt.color = color;
                fTxt.color = color;
                break;
        }
    }
    void SetInitLineRenderer()
    {
        lineRendererSinWave.positionCount = 100;
        lineRendererSinWave.startWidth = .2f;
        lineRendererSinWave.endWidth = .2f;
        lineRendererSinWave.useWorldSpace = false;

        lineRendererAlpha.positionCount = 50;
        lineRendererAlpha.startWidth = .2f;
        lineRendererAlpha.endWidth = .2f;

        lineRenderer.positionCount = 2;
        lineRenderer.widthMultiplier = 10f;
        lineRenderer.startWidth = .5f;
        lineRenderer.endWidth = .5f;
        lineRenderer.textureMode = LineTextureMode.Tile;
    }
    private void Update()
    {
        lineRenderer.SetPosition(0, sourceS.position);
        lineRenderer.SetPosition(1, carPos.position);

        WatchedToggleAutoCar();
        SinWave();
        CheckForObstacles();
    }
    void WatchedToggleAutoCar()
    {
        if (posCar.value == posCar.maxValue)
        {
            isToggleAutoCar = false;
            toggleAutoCar.isOn = false;
            toggleAutoCar.interactable = false;
            SetHighlight(false, 0);
        }
        else
        {
            toggleAutoCar.interactable = true;
        }
        if (isToggleAutoCar && vValue > 0)
        {
            posCar.value += Time.deltaTime * vValue;
            SetHighlight(true, 0);
        }
    }
    void OnSliderFoValueChanged(float arg0)
    {
        foValue = arg0;
        SetFoValueText();
        fDoppler();
    }
    void SetLamdaValue()
    {
        lamdaValue = (float)c / (float)(foValue * Math.Pow(10, 6));
        SetLamdaText();
    }
    void SetFoValueText()
    {
        if (foValue < 1000)
            foValueTxt.text = foValue.ToString("0") + " MHz";
        else
        {
            float foValueDisplay = foValue / 1000;
            foValueTxt.text = FormatString(foValueDisplay) + " GHz";
        }
    }
    void OnSliderVValueChanged(float arg0)
    {
        vValue = (int)arg0;
        SetVValueText();
        fDoppler();
    }
    void OnSliderPosCarChanged(float arg0)
    {
        // Debug.Log("aloo");
        MoveCar(arg0);
        MoveSouceS(arg0);
        fDoppler();
    }
    void OnToggleAutoCarChanged(bool arg0)
    {
        isToggleAutoCar = arg0;
        if (!arg0)
            SetHighlight(false, 0);
    }
    void MoveCar(float arg0)
    {
        car.transform.position = new Vector3(arg0, car.transform.position.y, car.transform.position.z);
    }
    void MoveCam()
    {
        // Vector3 camPos = Camera.main.transform.position;
        // float posZ = 0;
        // float maxPos = 100;

        // if (arg0 >= -camPosZInit && arg0 <= camPosZInit)
        // {
        //     posZ = camPosZInit;
        // }
        // else
        // {
        //     posZ = camPosZInit - (maxPos - Math.Abs(camPosZInit)) / (posCar.maxValue) * Math.Abs(arg0);
        // }
        // Camera.main.transform.position = new Vector3(camPos.x, camPos.y, posZ);

        Quaternion rotation = Quaternion.Euler(-30, -((float)alphaAngle - 90), 0);
        // Debug.Log(alphaAngle);

        Vector3 direction = new Vector3(0, 40f, -directionToTarget);

        Vector3 newPosition = rotation * direction + car.transform.position;

        cam.transform.position = newPosition;

        cam.transform.LookAt(car.transform.position + new Vector3(0, 10, 0));
    }
    void MoveSouceS(float arg0)
    {
        float posY = 0;
        if (arg0 >= -camPosZInit && arg0 <= camPosZInit)
        {
            posY = sourceSPosYInit;
        }
        else
        {
            posY = sourceSPosYInit / posCar.maxValue * (posCar.maxValue - Math.Abs(arg0)) + carPosYInit;
        }
        sourceS.position = new Vector3(sourceS.position.x, posY, sourceS.position.z);

        float lcScaleAnten = (posY) / sourceSPosYInit * antenScaleInit;

        anten.localScale = new Vector3(lcScaleAnten, lcScaleAnten, lcScaleAnten);
    }
    void fDoppler()
    {
        posSToCar = carPos.position - sourceS.position;

        carToArrowHead = -arrowHead.position + carPos.position;

        alphaAngle = Vector3.Angle(carToArrowHead, posSToCar);

        fDopplerValue = vValue / c * foValue * Math.Cos(alphaAngle * Math.PI / 180) * Math.Pow(10, 6);

        DrawAlphaAngle();
        SetAlphaText();
        SetFText();
        SetFRText();
        SetLamdaValue();
        MoveCam();

        double fDoppler180 = vValue / c * foValue * Math.Pow(10, 6);

        OnChartChange?.Invoke((float)fDoppler180, (int)(posCar.value + 90));
    }
    void SetVValueText()
    {
        vValueTxt.text = FormatString(vValue * 3.6f) + " km/h";
    }
    void SetAlphaText()
    {
        alphaTxt.text = "α = " + FormatString((float)alphaAngle) + "°";
    }
    void SetFText()
    {
        fTxt.text = "f = " + FormatString((float)fDopplerValue) + " Hz";
    }
    void SetLamdaText()
    {
        lamdaTxt.text = "λ = " + FormatString(lamdaValue) + " m";
    }
    void SetFRText()
    {
        float frValue = foValue + (float)fDopplerValue;
        if (frValue < 1000)
            fRTxt.text = "f<sub><size=100%>R</size></sub> = " + frValue.ToString("0") + " MHz";
        else
        {
            float foValueDisplay = frValue / 1000;
            fRTxt.text = "f<sub><size=100%>R</size></sub> = " + FormatString(foValueDisplay) + " GHz";
        }
    }
    string FormatString(float value)
    {
        value = (float)Math.Round(value, 2); // làm tròn giá trị đến 3 chữ số thập phân

        if (value % 1 != 0) // Kiểm tra là số thực
            return value.ToString("0.00");
        else
            return value.ToString("0");
    }
    void SinWave()
    {
        Vector3 startPos = lineRendererSinWave.transform.InverseTransformPoint(sourceS.position);
        Vector3 endPos = lineRendererSinWave.transform.InverseTransformPoint(carPos.position);

        float amplitude = 2.5f;
        float frequency = 4f;

        Vector3 direction = (endPos - startPos).normalized;

        int points = lineRendererSinWave.positionCount;
        for (int i = 0; i < points; i++)
        {
            float t = (float)i / (points - 1);
            Vector3 position = Vector3.Lerp(startPos, endPos, t);

            if (i < points / 2 + 1)
                amplitude = 2.5f;
            else
                amplitude = 2.5f * (posCar.maxValue - Math.Abs(posCar.value)) / posCar.maxValue;

            // Debug.Log(amplitude);
            float sineOffset = amplitude * Mathf.Sin(t * frequency * Mathf.PI * 2);
            Vector3 perpendicular = Vector3.Cross(direction, Vector3.forward).normalized;
            position += perpendicular * sineOffset;

            lineRendererSinWave.SetPosition(i, position);
        }
    }
    void CheckForObstacles()
    {
        ResetTransparency();
        Vector3 directionToCamera = cam.transform.position - car.transform.position;
        float distanceToCamera = directionToCamera.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(car.transform.position, directionToCamera, distanceToCamera, obstacleLayer);
        MeshRenderer objectRenderer = null;
        foreach (RaycastHit hit in hits)
        {
            objectRenderer = hit.collider.GetComponent<MeshRenderer>();
            // Debug.Log(objectRenderer);
            if (objectRenderer != null)
            {
                if (!transparentObjects.Contains(objectRenderer))
                {
                    SetObjectTransparent(objectRenderer);
                    transparentObjects.Add(objectRenderer);
                }
            }
        }
    }
    void SetObjectTransparent(MeshRenderer obj)
    {
        Material[] tempMat = obj.materials;

        for (int i = 0; i < obj.materials.Length; i++)
        {
            tempMat[i] = materialTransparent;
        }

        obj.materials = tempMat;
    }
    void ResetTransparency()
    {
        foreach (Renderer obj in transparentObjects)
        {
            Material[] tempMat = obj.materials;

            for (int i = 0; i < obj.materials.Length; i++)
            {
                tempMat[i] = materialNormal;
            }

            obj.materials = tempMat;
        }

        transparentObjects.Clear();
    }
    void DrawAlphaAngle()
    {
        Vector3 endPos = carPos.position - posSToCar.normalized * 4f;
        Vector3 startPos = carPos.position - carToArrowHead.normalized * 4f;

        // Calculate the direction vectors
        Vector3 dir1 = (endPos - startPos).normalized;
        Vector3 dir2 = Quaternion.Euler(0, 0, 90) * dir1;

        // Calculate the center of the arc
        Vector3 center = (startPos + endPos) / 2.0f + dir2 * 4;

        for (int i = 0; i < lineRendererAlpha.positionCount; i++)
        {
            float t = (float)i / lineRendererAlpha.positionCount;
            Vector3 point = Vector3.Slerp(startPos - center, endPos - center, t) + center;
            lineRendererAlpha.SetPosition(i, point);
        }

        alphaSymbol.transform.position =
            -(carPos.position - lineRendererAlpha.GetPosition(lineRendererAlpha.positionCount / 2 + 1)).normalized * 2
            + lineRendererAlpha.GetPosition(lineRendererAlpha.positionCount / 2 + 1);

        if (alphaAngle < 10) alphaSymbol.SetActive(false);
        else alphaSymbol.SetActive(true);
    }
}
