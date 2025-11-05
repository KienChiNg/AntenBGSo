using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationControllerDFlipFlop: AnimationController
{

    public LineRendererController lineCLK;
    public LineRendererController lineD;
    public LineRendererController lineQ;
    public Toggle toggleD;
    float current = 0;
    bool isDown = true;

    private void Start()
    {

        toggleD.onValueChanged.AddListener(delegate {
            lineD.currentValue = (lineD.currentValue + 1) % 2;
        });
    }

    // Update is called once per frame
    void Update()
    {
        CLKLine();
        QLine();
        //Debug.Log(references.objLists[0].GetComponent<TextMeshProUGUI>());
        references.objLists[0].GetComponent<TextMeshProUGUI>().text = lineD.currentValue.ToString();
        references.objLists[1].GetComponent<TextMeshProUGUI>().text = lineCLK.currentValue.ToString();
        references.objLists[2].GetComponent<TextMeshProUGUI>().text = lineQ.currentValue.ToString();
        //

    }

    private void QLine()
    {
        if(lineCLK.currentValue == 0)
        {
            isDown = true;
        }

        if (isDown && lineCLK.currentValue == 1)
        {
            lineQ.currentValue = lineD.currentValue;
            isDown = false;
        }
    }

    public void CLKLine()
    {
        lineCLK.currentValue = Mathf.Abs(Mathf.FloorToInt(Mathf.Cos(current * 1.5f)));
        current += Time.deltaTime;
    }

}
