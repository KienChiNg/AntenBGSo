using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationControllerBJTNL : AnimationController
{
    public Slider slider;
    public Toggle toggle;
    public Text sliderText;
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(delegate { OnSliderValueChange(); });
        toggle.onValueChanged.AddListener(delegate { OnToggleValueChange(); });
    }

    public void OnSliderValueChange()
    {
        float slideValue = slider.value;

        //round
        slideValue = Mathf.Round(slideValue * 100f) / 100f;
        sliderText.text = slideValue.ToString();
        references.animator.SetFloat("sliderSwitch", slideValue);
    }

    public void OnToggleValueChange()
    {
        references.animator.SetBool("toggleSwitch", toggle.isOn);
    }
}
