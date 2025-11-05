using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationControllerMOSFET : AnimationController
{
    public Slider slider1;
    public Text textSlide1;
    public Slider slider2;
    public Text textSlide2;
    //particle speed
    float maxCapSpeed = 2;
    float max_uP = 3;
    float min_uP = 1.5f;
    public float maxSpeed = 2;
    public float uP = 3;
    //scale
    //public GameObject[] scaleObs;
    public float scaleUpper1;
    public float scaleUpper2;
    public float scaleUpperPlat1;
    public float scaleUpperPlat2;
    public float scaleRedZ1;
    public float scaleRedZ2;
    public float scaleRedY1;
    public float scaleRedY2;

    //chart
    public RectTransform lineUDS;
    public RectTransform lineID;
    public RectTransform lineUGS;
    public RectTransform textUGS;

    // Start is called before the first frame update
    void Start()
    {
        slider1.onValueChanged.AddListener(delegate
        {
            OnSlider1Change();
        });

        slider2.onValueChanged.AddListener(delegate
        {
            OnSlider2Change();
        });

        CalParticleSpeed();
        references.particle.Play();

        calUDSline();
        calIDline();
        calUGSline();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSlider1Change()
    {
        textSlide1.text = (Mathf.Round(slider1.value * 100f) / 10f).ToString();
        //cal speed
        CalParticleSpeed();

        //cal scale
        float scaleUperPlat = CalScaleUpperPlat();
        Vector3 localScale = references.objLists[1].transform.localScale;
        references.objLists[1].transform.localScale = new Vector3(localScale.x, scaleUperPlat, localScale.z);

        //scale red
        float scaleRedZ = CalScaleRedZ();
        float scaleRedY = CalScaleRedY();

        Vector3 localScale1 = references.objLists[3].transform.localScale;
        references.objLists[3].transform.localScale = new Vector3(localScale1.x, localScale1.y, scaleRedZ);

        Vector3 localScale2 = references.objLists[4].transform.localScale;
        references.objLists[4].transform.localScale = new Vector3(localScale2.x, localScale2.y, scaleRedZ);

        Vector3 localScale3 = references.objLists[5].transform.localScale;
        references.objLists[5].transform.localScale = new Vector3(localScale3.x, scaleRedY, scaleRedZ);

        calUDSline();
        calIDline();
    }

    public void OnSlider2Change()
    {
        textSlide2.text = (1 + Mathf.Round(slider2.value * -4f * 10f) / 10f).ToString();

        //cal speed
        maxSpeed = maxCapSpeed * (1 - slider2.value);
        uP = max_uP - slider2.value * (max_uP - min_uP);

        CalParticleSpeed();

        //scale upper
        float scaleUper = CalScaleUpper();
        Vector3 localScale = references.objLists[0].transform.localScale;
        references.objLists[0].transform.localScale = new Vector3(localScale.x, scaleUper, localScale.z);
        references.objLists[1].transform.position = Vector3.Lerp(references.objLists[1].transform.position, references.objLists[2].transform.position, 1f);

        calIDline();
        calUGSline();
    }

    public void CalParticleSpeed()
    {
        if (slider1.value == 0)
        {
            references.particle.playbackSpeed = 0.005f * 10 * maxSpeed / uP;
            return;
        }

        if (slider1.value * 10 <= uP)
        {
            references.particle.playbackSpeed = slider1.value * 10 * maxSpeed / uP;
        }
        else
        {
            references.particle.playbackSpeed = maxSpeed;
        }
    }

    public float CalScaleUpper()
    {
        return scaleUpper2 - slider2.value * (scaleUpper2 - scaleUpper1);
    }

    public float CalScaleUpperPlat()
    {
        return scaleUpperPlat1 + slider1.value * (scaleUpperPlat2 - scaleUpperPlat1);
    }

    public float CalScaleRedZ()
    {
        return scaleRedZ1 + slider1.value * (scaleRedZ2 - scaleRedZ1);
    }

    public float CalScaleRedY()
    {
        return scaleRedY1 + slider1.value * (scaleRedY2 - scaleRedY1);
    }




    public void calUDSline()
    {
        float UDSpos = 14f + slider1.value * (127f - 14f);
        lineUDS.anchoredPosition = new Vector3(UDSpos, lineUDS.anchoredPosition.y);
    }

    public void calIDline()
    {
        float IDpos = 2.5f + references.particle.playbackSpeed * (75f - 3f) / 2;
        lineID.anchoredPosition = new Vector3(lineID.anchoredPosition.x, IDpos);
    }

    public void calUGSline()
    {
        float UGSscaleY = 1 - slider2.value * (1f - 0.04f);
        float UGSscaleX = 1 - slider2.value * (1f - 0.1f);
        lineUGS.localScale = new Vector3(UGSscaleX, UGSscaleY, lineUGS.localScale.z);

        float UGSpos = 85f - slider2.value * (85f - 13f);
        textUGS.anchoredPosition = new Vector3(textUGS.anchoredPosition.x, UGSpos);
    }
}
