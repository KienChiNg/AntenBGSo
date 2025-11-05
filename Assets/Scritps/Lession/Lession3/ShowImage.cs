using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowImage : MonoBehaviour //DatNT
{
    [System.Serializable]
    public class Content
    {
        public Sprite sprite;
        public string type;
    }
    public List<Content> contents;
    public Slider slider;
    public Image image1;
    public TMP_Text contentTxt;
    float epsilon = 1e-5f;
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(showImage);
        showImage(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void showImage(float value)
    {
        int t = 0;
        if (Mathf.Abs(value) < epsilon)
        {
            value = 0f;
        }
        // if (slider.value < 0.02f && slider.value > -0.02f)
        //     slider.value = 0;
        // Debug.Log(value);
        if (value > 0)
        {
            t = 0;
        }
        else if (value == 0)
        {
            t = 1;
        }
        else if (value > -0.04 / 10 && value < 0)
        {
            t = 2;
        }
        else if (value > -0.157 / 10 && value < -0.04 / 10)
        {
            t = 3;
        }
        else
        {
            t = 4;
        }
        // Debug.Log(t);
        image1.sprite = contents[t].sprite;
        contentTxt.text = $"- <color=red>Loại khúc xạ:</color> {contents[t].type}";
    }
}
