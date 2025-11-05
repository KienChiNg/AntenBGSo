using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderDothi : MonoBehaviour
{
    Text value;
    public Slider slider;
    public GameObject line;
    float scaleY;

    // Start is called before the first frame update
    void Start()
    {
        value = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        value.text = (slider.value).ToString();
        scaleY= 1.2f * (slider.value / (slider.maxValue - slider.minValue));
        line.transform.localScale = new Vector3(line.transform.localScale.x, scaleY, line.transform.localScale.z);
    }

}
