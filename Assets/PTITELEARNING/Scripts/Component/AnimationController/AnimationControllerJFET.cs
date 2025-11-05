using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationControllerJFET : AnimationController
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
    public float scale1;
    public float scale2;
    public float scale3;

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
        float scale = CalScale();
        for (int i = 0; i < references.objLists.Length; i++)
        {
            Vector3 localScale = references.objLists[i].transform.localScale;
            references.objLists[i].transform.localScale = new Vector3(localScale.x, localScale.y, scale);
        }

        calUDSline();
        calIDline();

    }

    public void OnSlider2Change()
    {
        textSlide2.text = (Mathf.Round(slider2.value * -2.5f * 10f) / 10f).ToString();

        //cal speed
        maxSpeed = maxCapSpeed * (1 - slider2.value);
        uP = max_uP - slider2.value * (max_uP - min_uP);

        CalParticleSpeed();

        float scale = CalScale(); 
        for(int i = 0; i < references.objLists.Length; i ++)
        {
            Vector3 localScale = references.objLists[i].transform.localScale;
            references.objLists[i].transform.localScale = new Vector3(localScale.x, localScale.y, scale);
        }

        calIDline();
        calUGSline();
    }

    public void CalParticleSpeed()
    {
        if(slider1.value == 0)
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

    public float CalScale()
    {
        if (slider1.value == 0)
        {
            return scale1 + 0.01f * 10 * (scale2 - scale1) / uP;
        }

        if (slider1.value * 10 <= uP)
        {
            return scale1 + slider1.value * 10 * (scale2 - scale1) / uP;
        }
        else
        {
            return scale2 + (slider1.value * 10 - uP) * (scale2 - scale1) / (10 - uP);
        }

        
    }

    public void calUDSline()
    {
        float UDSpos = 14f+slider1.value*(127f-14f);
        lineUDS.anchoredPosition = new Vector3(UDSpos, lineUDS.anchoredPosition.y);
    }

    public void calIDline()
    {
        float IDpos = 2.5f+ references.particle.playbackSpeed*(75f - 3f)/2;
        lineID.anchoredPosition = new Vector3(lineID.anchoredPosition.x, IDpos);
    }

    public void calUGSline()
    {
        float UGSscaleY = 1- slider2.value * (1f - 0.04f);
        float UGSscaleX = 1 - slider2.value * (1f - 0.1f);
        lineUGS.localScale = new Vector3(UGSscaleX, UGSscaleY, lineUGS.localScale.z);

        float UGSpos = 85f - slider2.value * (85f - 13f);
        textUGS.anchoredPosition = new Vector3(textUGS.anchoredPosition.x, UGSpos);
    }

}
