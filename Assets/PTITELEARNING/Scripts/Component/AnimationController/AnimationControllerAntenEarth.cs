using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationControllerAntenEarth : AnimationController
{
    public Slider slider1;
    public Text textSlide1;

    public float minDis = 0;
    public float maxDis = -0.9f;
    // float distanceRate = 0.6f;
    // float posA = 0.8f;


    // Start is called before the first frame update
    void Start()
    {
        OnSlider1Change();

        slider1.onValueChanged.AddListener(delegate
        {
            OnSlider1Change();
        });

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSlider1Change()
    {
        textSlide1.text = (Mathf.Round(slider1.value * 100f) / 10f).ToString();


        //cal B
        Vector3 localPositionB = references.objLists[0].transform.localPosition;
        references.objLists[0].transform.localPosition = new Vector3(minDis - slider1.value * (minDis - maxDis), localPositionB.y, localPositionB.z);

    }

}
