using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationControllerAntenCity : AnimationController
{
    public Slider sliderR;
    public TMP_Text textSlideR;

    public Slider sliderF;
    public TMP_Text textSlideF;

    public Slider sliderHt;
    public TMP_Text textSlideHt;

    public Slider sliderHr;
    public TMP_Text textSlideHr;

    public Slider Ebar;
    public float EbarMaxValue = 1.7f;

    public float Pt = 1;
    public float Gt = 100;
    public float R = 1;
    float lamda;
    float E;
    float B;
    float deltaR;


    public Text wavestep;
    public TMP_InputField PtText;
    public TMP_InputField GtText;
    public TMP_InputField RText;
    public TMP_Text EText;

    public float minDis = -0.4f;
    public float maxDis = -1.7f;
    float distanceRate;
    // float posA = 0.8f;

    float antenScaleMin = 0.5f;
    float antenScaleMax = 1.5f;
    [SerializeField] private List<SelectSlider> selectSliders;
    [SerializeField] private List<GameObject> lineRenderers;
    private bool isSelect;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        calculate();
        OnSliderRChange();
        OnSliderFChange();
        OnSliderHtChange();
        OnSliderHrChange();

        sliderR.onValueChanged.AddListener(delegate
        {
            OnSliderRChange();
        });

        sliderF.onValueChanged.AddListener(delegate
        {
            OnSliderFChange();
        });

        sliderHt.onValueChanged.AddListener(delegate
        {
            OnSliderHtChange();
        });
        sliderHr.onValueChanged.AddListener(delegate
        {
            OnSliderHrChange();
        });

        SetTextResult();

        PtText.onValueChanged.AddListener(delegate
        {
            InputValue();
        });
        GtText.onValueChanged.AddListener(delegate
        {
            InputValue();
        });
        RText.onValueChanged.AddListener(delegate
        {
            InputValue();
        });

        foreach (SelectSlider selectSlider in selectSliders)
        {
            selectSlider.onSelect += SetIsSelect;
        }

        // Ebar.maxValue = EbarMaxValue;
    }

    private void SetIsSelect(bool obj)
    {
        isSelect = obj;
        if (!obj)
        {
            foreach (GameObject gameObject in lineRenderers)
                gameObject.SetActive(true);
            calculate();
        }
        else
        {
            foreach (GameObject gameObject in lineRenderers)
                gameObject.SetActive(false);
        }
        // // else
    }



    // Update is called once per frame
    void Update()
    {
    }
    void OnSliderValueChanged(Slider slider)
    {
        // Làm tròn giá trị slider về bội số của 0.5
        float step = 0.5f;
        slider.value = Mathf.Round(slider.value / step) * step;
    }
    public void OnSliderRChange()
    {
        OnSliderValueChanged(sliderR);
        textSlideR.text = (Mathf.Round((sliderR.value * 2) * 10.0f) * 0.1f).ToString("0.0") + " (km)";


        //cal B
        Vector3 localPositionB = references.objLists[0].transform.localPosition;
        references.objLists[0].transform.localPosition = new Vector3(minDis - (minDis - maxDis) * (sliderR.value / sliderR.maxValue), localPositionB.y, localPositionB.z);

        calculate();
    }

    public void OnSliderFChange()
    {
        OnSliderValueChanged(sliderF);

        textSlideF.text = (Mathf.Round((sliderF.value) * 10.0f) * 0.1f).ToString("0.0") + " (GHz)";
        calculate();
    }

    public void OnSliderHtChange()
    {
        OnSliderValueChanged(sliderHt);

        textSlideHt.text = (Mathf.Round((sliderHt.value * 20) * 10.0f) * 0.1f).ToString("0.0") + " (m)";
        calculate();
    }

    public void OnSliderHrChange()
    {
        OnSliderValueChanged(sliderHr);

        textSlideHr.text = (Mathf.Round((sliderHr.value * 20) * 10.0f) * 0.1f).ToString("0.0") + " (m)";
        calculate();
    }
    public void InputValue()
    {
        try
        {
            Pt = float.Parse(PtText.text);
            Gt = float.Parse(GtText.text);
            R = float.Parse(RText.text);
        }
        catch
        {
            Debug.Log("KC_ERROR");
            // EText.text = "E = NaN" + " (mV/m)";
        }
        calculate();
    }
    private void SetTextResult()
    {
        PtText.text = Pt.ToString();
        GtText.text = Gt.ToString();
        RText.text = R.ToString();
    }
    public void calculate()
    {
        lamda = 3 * 100000 / ((sliderF.value * 1000000000) / 1000);
        deltaR = 2 * sliderHr.value * 20 * sliderHt.value * 20 / (sliderR.value * 2 * 1000);
        B = Mathf.PI + 2 * Mathf.PI * deltaR / lamda;
        E = 173 * (Mathf.Sqrt(Pt * Gt) / (sliderR.value * 2 * 1000)) * (Mathf.Sqrt(1 + 2 * R * Mathf.Cos(B) + Mathf.Pow(R, 2)));

        // Debug.Log(deltaR + " " + lamda + " " + B + " " + E);
        wavestep.text = "λ = " + (Mathf.Round(lamda * 1000.0f) * 0.001f).ToString() + " (m)";
        // PtText.text = Pt.ToString();
        // GtText.text = Gt.ToString();
        // RText.text = Pt.ToString();

        distanceRate = sliderHt.value * 20 / (sliderHt.value * 20 + sliderHr.value * 20);


        //cal C
        Vector3 localPositionC = references.objLists[1].transform.localPosition;
        references.objLists[1].transform.localPosition = new Vector3(references.objLists[0].transform.localPosition.x * distanceRate, localPositionC.y, localPositionC.z);

        //calScale
        Vector3 localScaleHt = references.objLists[2].transform.localScale;
        references.objLists[2].transform.localScale = new Vector3(0.5f, 0.5f + (sliderHt.value / sliderHt.maxValue) * (antenScaleMax - antenScaleMin), 0.5f);
        Vector3 localScaleHr = references.objLists[3].transform.localScale;
        references.objLists[3].transform.localScale = new Vector3(0.5f, 0.5f + (sliderHr.value / sliderHr.maxValue) * (antenScaleMax - antenScaleMin), 0.5f);

        float ranDis = UnityEngine.Random.Range((references.objLists[0].transform.localPosition.x + 0.1f) / 2, -0.1f);
        // Debug.Log("" + ranDis);
        references.objLists[4].transform.localPosition = new Vector3(ranDis, localPositionC.y, localPositionC.z);

        references.objLists[5].transform.localPosition = PointDetermination(references.topAnten[0].localPosition, references.objLists[4].transform.localPosition);

        ranDis = UnityEngine.Random.Range((references.objLists[0].transform.localPosition.x + 0.1f) / 2, (references.objLists[0].transform.localPosition.x + 0.1f));
        // Debug.Log("" + ranDis);
        references.objLists[6].transform.localPosition = new Vector3(ranDis, localPositionC.y, localPositionC.z);

        references.objLists[7].transform.localPosition = PointDetermination(references.topAnten[0].localPosition, references.objLists[6].transform.localPosition);

        EText.text = "E<sub>h</sub> = " + (Mathf.Round(E * 1000.0f) * 0.001f).ToString("0.000") + " (mV/m)";
        // Ebar.value = E;
    }

    private Vector3 PointDetermination(Vector3 startPoint, Vector3 hitPoint)
    {
        Vector3 incomingDirection = (hitPoint - startPoint).normalized;

        Vector3 normalRay = (hitPoint - new Vector3(hitPoint.x, 1, hitPoint.z)).normalized;

        // Vector3 normalRay = (new Vector3(hitPoint,1,hitPoint)).normalized;

        Vector3 reflectionDirection = Vector3.Reflect(incomingDirection, normalRay);

        Vector3 pointAtUnitDistance = hitPoint + reflectionDirection * 0.2f;

        return pointAtUnitDistance;
    }

}
