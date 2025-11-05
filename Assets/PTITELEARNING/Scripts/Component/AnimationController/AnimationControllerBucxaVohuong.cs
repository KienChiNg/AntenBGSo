using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimationControllerBucxaVohuong : AnimationController
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
    float E;
    float P;
    float L;
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

        OnSliderNChange();
        OnSliderMChange();
        OnSliderPChange();
        OnSliderFChange();

        sliderN.onValueChanged.AddListener(delegate
        {
            OnSliderNChange();
        });

        sliderM.onValueChanged.AddListener(delegate
        {
            OnSliderMChange();
        });

        sliderP.onValueChanged.AddListener(delegate
        {
            OnSliderPChange();
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

    public void OnSliderNChange()
    {
        textSliderN.text = (sliderN.value ).ToString();

        float maxDis = -0.8f;
        Vector3 localPositionN = references.objLists[1].transform.localPosition;
        references.objLists[1].transform.localPosition = 
            new Vector3(
                localPositionN.y, 
                localPositionN.y, 
                sliderN.value / sliderN.maxValue * maxDis);
        calculateN();
    }

    public void OnSliderMChange()
    {
        textSliderM.text = (sliderM.value ).ToString();
        float maxDis = -0.8f;
        Vector3 localPositionM = references.objLists[0].transform.localPosition;
        //Vector3 localPositionN = references.objLists[1].transform.localPosition;
        //Vector3 localPositionP = references.objLists[2].transform.localPosition;
        references.objLists[0].transform.localPosition = new Vector3(localPositionM.y, localPositionM.y, sliderM.value / sliderM.maxValue * maxDis);
        //references.objLists[1].transform.localPosition = new Vector3(localPositionN.y, localPositionN.y, sliderM.value / sliderM.maxValue * maxDis);
        //references.objLists[2].transform.localPosition = new Vector3(localPositionP.y, localPositionP.y, sliderM.value / sliderM.maxValue * maxDis);

        calculateM();
    }

    public void OnSliderPChange()
    {
        textSliderP.text = (sliderP.value ).ToString();
        float maxDis = -0.8f;
        //Vector3 localPositionM = references.objLists[0].transform.localPosition;
        //Vector3 localPositionN = references.objLists[1].transform.localPosition;
        Vector3 localPositionP = references.objLists[2].transform.localPosition;
        //references.objLists[0].transform.localPosition = new Vector3(localPositionM.y, localPositionM.y, sliderP.value / sliderP.maxValue * maxDis);
        //references.objLists[1].transform.localPosition = new Vector3(localPositionN.y, localPositionN.y, sliderP.value / sliderP.maxValue * maxDis);
        references.objLists[2].transform.localPosition = new Vector3(localPositionP.y, localPositionP.y, sliderP.value / sliderP.maxValue * maxDis);

        calculateP();
    }

    public void OnSliderFChange()
    {
        textSlideF.text = (sliderF.value).ToString();
        calculateN();
        calculateM();
        calculateP();
    }

    public void calculateN()
    {
        lamda = 3 * 100000 / ((sliderF.value * 1000000000) / 1000);

        E = 173 * Mathf.Sqrt(Pzigma) / sliderN.value;

        P = Mathf.Pow((lamda / (4 * Mathf.PI * sliderN.value  * 1000)), 2) * Pzigma * 1000;

        L = Pzigma * 1000 / P;


        wavestep.text = "λ = " + (Mathf.Round(lamda * 1000.0f) * 0.001f).ToString() + " (m)";
        Ptong.text = "P<sub>Σ</sub> = " + Pzigma.ToString() + " (kW)";

        EN.text = " E= " + (E).ToString() + " (mV/m)";
        PN.text = " P= " + (Mathf.Round(P * 1000000000 * 100.0f) * 0.01f).ToString() + "x10<sup>-9</sup>" + " (W)";
        LN.text = " L= " + (Mathf.Round((L / 1000000000) * 100.0f) * 0.01f).ToString().ToString() + "x10<sup>^9</sup>";

        PdbN.text = "P<sub>dBm</sub>= " + (Mathf.Round((10 * Mathf.Log((P * 1000 / 1), 10)) * 10.0f) * 0.1f).ToString() + " (dBm)";
        LdbN.text = "L<sub>db</sub>= " + (Mathf.Round((10 * Mathf.Log(L, 10)) * 100.0f) * 0.01f).ToString() + " (dB)";




        // Debug.Log("E=" + E);
        // Debug.Log("P=" + P);
        // Debug.Log("L=" + L);
        // Debug.Log("Lamda=" + lamda);
    }

    public void calculateM()
    {
        lamda = 3 * 100000 / ((sliderF.value * 1000000000) / 1000);

        E = 173 * Mathf.Sqrt(Pzigma) / sliderM.value;

        P = Mathf.Pow((lamda / (4 * Mathf.PI * sliderM.value  * 1000)), 2) * Pzigma * 1000;

        L = Pzigma * 1000 / P;


        wavestep.text = "λ = " + (Mathf.Round(lamda * 1000.0f) * 0.001f).ToString() + " (m)";
        Ptong.text = "P<sub>Σ</sub> = " + Pzigma.ToString() + " (kW)";

        EM.text = " E= " + (E).ToString() + " (mV/m)";
        PM.text = " P= " + (Mathf.Round(P * 1000000000 * 100.0f) * 0.01f).ToString() + "x10<sup>-9</sup>" + " (W)";
        LM.text = " L= " + (Mathf.Round((L / 1000000000) * 100.0f) * 0.01f).ToString().ToString() + "x10<sup>9</sup>";

        PdbM.text = "P<sub>dBm</sub>= " + (Mathf.Round((10 * Mathf.Log((P * 1000 / 1), 10)) * 10.0f) * 0.1f).ToString() + " (dBm)";
        LdbM.text = "L<sub>db</sub>= " + (Mathf.Round((10 * Mathf.Log(L, 10)) * 100.0f) * 0.01f).ToString() + " (dB)";

    }

    public void calculateP()
    {
        lamda = 3 * 100000 / ((sliderF.value * 1000000000) / 1000);

        E = 173 * Mathf.Sqrt(Pzigma) / sliderP.value;

        P = Mathf.Pow((lamda / (4 * Mathf.PI * sliderP.value  * 1000)), 2) * Pzigma * 1000;

        L = Pzigma * 1000 / P;


        wavestep.text = "λ = " + (Mathf.Round(lamda * 1000.0f) * 0.001f).ToString() + " (m)";
        Ptong.text = "P<sub>Σ</sub> = " + Pzigma.ToString() + " (kW)";

        EP.text = " E= " + (E).ToString() + " (mV/m)";
        PP.text = " P= " + (Mathf.Round(P * 1000000000 * 100.0f) * 0.01f).ToString() + "x10<sup>-9</sup>" + " (W)";
        LP.text = " L= " + (Mathf.Round((L / 1000000000) * 100.0f) * 0.01f).ToString().ToString() + "x10<sup>9</sup>";

        PdbP.text = "P<sub>dBm</sub>= " + (Mathf.Round((10 * Mathf.Log((P * 1000 / 1), 10)) * 10.0f) * 0.1f).ToString() + " (dBm)";
        LdbP.text = "L<sub>db</sub>= " + (Mathf.Round((10 * Mathf.Log(L, 10)) * 100.0f) * 0.01f).ToString() + " (dB)";




        // Debug.Log("E=" + E);
        // Debug.Log("P=" + P);
        // Debug.Log("L=" + L);
        // Debug.Log("Lamda=" + lamda);
    }
    private void SetM()
    {
        EM.text = " E= " + (E).ToString() + " (mV/m)";
        PM.text = " P= " + (Mathf.Round(P * 1000000000 * 100.0f) * 0.01f).ToString() + "x10<sup>-9</sup>" + " (W)";
        LM.text = LN.text = LP.text = " L= " + (Mathf.Round((L / 1000000000) * 100.0f) * 0.01f).ToString().ToString() + "x10<sup>9</sup>";

        PdbM.text = PdbN.text = PdbP.text = "= " + (Mathf.Round((10 * Mathf.Log((P * 1000 / 1), 10)) * 10.0f) * 0.1f).ToString() + " (dBm)";
        LdbM.text = LdbN.text = LdbP.text = "= " + (Mathf.Round((10 * Mathf.Log(L, 10)) * 100.0f) * 0.01f).ToString() + " (dB)";
    }
}
