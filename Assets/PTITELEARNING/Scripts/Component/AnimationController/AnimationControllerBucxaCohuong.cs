using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimationControllerBucxaCohuong : AnimationController
{
    public Slider sliderN;
    public Text textSliderN;
    public Slider sliderM;
    public Text textSliderM;
    public Slider sliderP;
    public Text textSliderP;

    public Slider sliderF;
    public Text textSlideF;

    public float Pzigma = 1f;
    public float Dr = 100f;

    public float DtM = 100f;
    public float DtN = 50f;
    public float DtP = 0f;
    float EvalueM;
    float PvalueM;
    float LvalueM;
    float EvalueN;
    float PvalueN;
    float LvalueN;
    float EvalueP;
    float PvalueP;
    float LvalueP;
    float lamda;

    public TMP_Text EM;
    public TMP_Text PM;
    public TMP_Text LM;
    public TMP_Text PdbM;
    public TMP_Text LdbM;

    public TMP_Text EN;
    public TMP_Text PN;
    public TMP_Text LN;
    public TMP_Text PdbN;
    public TMP_Text LdbN;

    public TMP_Text EP;
    public TMP_Text PP;
    public TMP_Text LP;
    public TMP_Text PdbP;
    public TMP_Text LdbP;

    public TMP_Text wavestep;
    public TMP_Text Ptong;



    // Start is called before the first frame update
    void Start()
    {

        OnSliderRChange(sliderN, textSliderN);
        OnSliderRChange(sliderM, textSliderM);
        OnSliderRChange(sliderP, textSliderP);
        OnSliderFChange();

        sliderN.onValueChanged.AddListener(delegate
        {
            OnSliderRChange(sliderN, textSliderN);
        });
        sliderM.onValueChanged.AddListener(delegate
        {
            OnSliderRChange(sliderM, textSliderM);
        });
        sliderP.onValueChanged.AddListener(delegate
        {
            OnSliderRChange(sliderP, textSliderP);
        });

        OnSliderFChange();

        sliderF.onValueChanged.AddListener(delegate
        {
            OnSliderFChange();
        });

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSliderRChange(Slider sliderR, Text textSlideR)
    {
        textSlideR.text = (sliderR.value).ToString();

        float maxDis = -0.8f;
        Vector3 localPositionM = references.objLists[0].transform.localPosition;
        Vector3 localPositionN = references.objLists[1].transform.localPosition;
        Vector3 localPositionP = references.objLists[2].transform.localPosition;
        references.objLists[0].transform.localPosition = new Vector3(localPositionM.y, localPositionM.y, sliderM.value / sliderM.maxValue * maxDis);
        references.objLists[1].transform.localPosition = new Vector3(localPositionN.y, localPositionN.y, sliderN.value / sliderN.maxValue * maxDis);
        references.objLists[2].transform.localPosition = new Vector3(localPositionP.y, localPositionP.y, sliderP.value / sliderP.maxValue * maxDis);

        calculate();
    }

    public void OnSliderFChange()
    {
        textSlideF.text = (sliderF.value).ToString();
        calculate();
    }

    public void calculate()
    {
        lamda = 3 * 100000 / ((sliderF.value * 1000000000) / 1000);

        //Diem M
        EvalueM = 173 * Mathf.Sqrt(DtM * Pzigma) / sliderM.value;
        PvalueM = Mathf.Pow((lamda / (4 * Mathf.PI * sliderM.value * 2 * 1000)), 2) * DtM * Dr * Pzigma * 1000;
        LvalueM = Pzigma * 1000 / PvalueM;

        //Diem N
        EvalueN = 173 * Mathf.Sqrt(DtN * Pzigma) / sliderN.value;
        PvalueN = Mathf.Pow((lamda / (4 * Mathf.PI * sliderN.value * 2 * 1000)), 2) * DtN * Dr * Pzigma * 1000;
        LvalueN = Pzigma * 1000 / PvalueN;

        //Diem P
        EvalueP = 173 * Mathf.Sqrt(DtP * Pzigma) / sliderP.value;
        PvalueP = Mathf.Pow((lamda / (4 * Mathf.PI * sliderP.value * 2 * 1000)), 2) * DtP * Dr * Pzigma * 1000;
        LvalueP = Pzigma * 1000 / PvalueP;


        wavestep.text = "λ = " + (Mathf.Round(lamda * 1000.0f) * 0.001f).ToString() + " (m)";
        Ptong.text = "P<sub>Σ</sub> = " + Pzigma.ToString() + " (kW)";

        //Show text Diem M
        EM.text = " E= " + (Mathf.Round(EvalueM * 1.0f) * 1f).ToString() + " (mV/m)";
        PM.text = " P= " + (Mathf.Round(PvalueM * 1000000000 * 100.0f) * 0.01f).ToString() + "x10<sup>-9</sup>" + " (W)";
        LM.text = " L= " + (Mathf.Round((LvalueM / 1000000000) * 100.0f) * 0.01f).ToString().ToString() + "x10<sup>9</sup>";

        PdbM.text = "P<sub>dBm</sub>= " + (Mathf.Round((10 * Mathf.Log((PvalueM * 1000 / 1), 10)) * 10.0f) * 0.1f).ToString() + " (dBm)";
        LdbM.text = "L<sub>db</sub>= " + (Mathf.Round((10 * Mathf.Log(LvalueM, 10)) * 100.0f) * 0.01f).ToString() + " (dB)";

        //Show text Diem N
        EN.text = " E= " + (Mathf.Round(EvalueN * 1.0f) * 1f).ToString() + " (mV/m)";
        PN.text = " P= " + (Mathf.Round(PvalueN * 1000000000 * 100.0f) * 0.01f).ToString() + "x10<sup>-9</sup>" + " (W)";
        LN.text = " L= " + (Mathf.Round((LvalueN / 1000000000) * 100.0f) * 0.01f).ToString().ToString() + "x10<sup>9</sup>";

        PdbN.text = "P<sub>dBm</sub>= " + (Mathf.Round((10 * Mathf.Log((PvalueN * 1000 / 1), 10)) * 10.0f) * 0.1f).ToString() + " (dBm)";
        LdbN.text = "L<sub>db</sub>= " + (Mathf.Round((10 * Mathf.Log(LvalueN, 10)) * 100.0f) * 0.01f).ToString() + " (dB)";

        //Show text Diem p
        EP.text = " E= " + (Mathf.Round(EvalueP * 1.0f) * 1f).ToString() + " (mV/m)";
        //PP.text = " P= " + (Mathf.Round(PvalueP * 1000000000 * 100.0f) * 0.01f).ToString() + "x10<sup>-9</sup>" + " (W)";
        //LP.text = " L= " + (Mathf.Round((LvalueP / 1000000000) * 100.0f) * 0.01f).ToString().ToString() + "x10<sup>9</sup>";

        //PdbP.text = "= " + (Mathf.Round((10 * Mathf.Log((PvalueP * 1000 / 1), 10)) * 10.0f) * 0.1f).ToString() + " (dBm)";
        //LdbP.text = "= " + (Mathf.Round((10 * Mathf.Log(LvalueP, 10)) * 100.0f) * 0.01f).ToString() + " (dB)";

        PP.text = " P= 0 (W)";
        LP.text = " L= infinity";

        PdbP.text = "";
        LdbP.text = "";


    }

}
