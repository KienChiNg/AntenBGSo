using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerWave : MonoBehaviour
{
    [SerializeField]
    private Toggle _toggleWave1;
    [SerializeField]
    private TMP_Dropdown _polarizationWave1;
    [SerializeField]
    private Slider _amplitudeWave1;
    [SerializeField]
    private TMP_Text _txtAmplitudeWave1;
    [SerializeField]
    private Slider _wavelengthWave1;
    [SerializeField]
    private TMP_Text _txtWavelengthWave1;
    [SerializeField]
    private Toggle _reverseDirection;

    [SerializeField]
    private Toggle _toggleWave2;
    [SerializeField]
    private TMP_Dropdown _polarizationWave2;
    [SerializeField]
    private Slider _amplitudeWave2;
    [SerializeField]
    private TMP_Text _txtAmplitudeWave2;
    [SerializeField]
    private Slider _wavelengthWave2;
    [SerializeField]
    private TMP_Text _txtWavelengthWave2;
    [SerializeField]
    private Slider _phaseDifferenceWave2;
    [SerializeField]
    private TMP_Text _txtPhaseDifferenceWave2;

    [SerializeField]
    private Toggle _toggleWave1AddWave2;

    [SerializeField]
    private Toggle _toggleMaterial;
    [SerializeField]
    private Slider _lengthMaterial;
    [SerializeField]
    private TMP_Text _txtLengthMaterial;
    [SerializeField]
    private Slider _extinctionCoefficientWave1;
    [SerializeField]
    private TMP_Text _txtExtinctionCoefficientWave1;
    [SerializeField]
    private Slider _refractionIndexWave1;
    [SerializeField]
    private TMP_Text _txtRefractionIndexWave1;
    [SerializeField]
    private Slider _extinctionCoefficientWave2;
    [SerializeField]
    private TMP_Text _txtExtinctionCoefficientWave2;
    [SerializeField]
    private Slider _refractionIndexWave2;
    [SerializeField]
    private TMP_Text _txtRefractionIndexWave2;
    [SerializeField]
    private Toggle _sameAsWave1;

    [SerializeField]
    private Toggle _hideCurvesComponents;
    [SerializeField]
    private Toggle _hideCurvesSuperposition;

    [SerializeField]
    private Slider _animationRate;
    [SerializeField]
    private TMP_Text _txtAnimationRate;

    public void Init()
    {
        _toggleWave1.isOn = true;
        // _polarizationWave1.value = 3;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = false;
        // _polarizationWave1.value = 0;
        _amplitudeWave2.value = 5;
        _wavelengthWave2.value = 4;
        _phaseDifferenceWave2.value = 0;


        _toggleWave1AddWave2.isOn = false;

        _toggleMaterial.isOn = false;
        _lengthMaterial.value = 16;
        _extinctionCoefficientWave1.value = 0;
        _extinctionCoefficientWave2.value = 0;
        _refractionIndexWave1.value = 0;
        _refractionIndexWave2.value = 0;
        _sameAsWave1.isOn = false;

        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;

        _animationRate.value = 30;
    }
    public void SetUpWave()
    {
        SetAllText();
        if (_toggleWave1.isOn && _toggleWave2.isOn)
        {
            _toggleWave1AddWave2.interactable = true;
        }
        else
        {
            _toggleWave1AddWave2.interactable = false;
            _toggleWave1AddWave2.isOn = false;
        }

        if (_sameAsWave1.isOn)
        {
            _extinctionCoefficientWave2.value = _extinctionCoefficientWave1.value;
            _refractionIndexWave2.value = _refractionIndexWave1.value;
        }

        DrawWave drawWave = LessionManager.instance.drawWave;
        drawWave.wave1 = _toggleWave1.isOn;
        drawWave.ampli1 = (int)_amplitudeWave1.value;
        drawWave.wwll1 = (int)_wavelengthWave1.value;
        drawWave.rev = _reverseDirection.isOn;

        drawWave.wave2 = _toggleWave2.isOn;
        drawWave.ampli2 = (int)_amplitudeWave2.value;
        drawWave.wwll2 = (int)_wavelengthWave2.value;
        drawWave.phase = (int)_phaseDifferenceWave2.value;

        drawWave.wave1AddWave2 = _toggleWave1AddWave2.isOn;

        drawWave.cMatOn = _toggleMaterial.isOn;
        drawWave.cmatlen = (int)_lengthMaterial.value;
        drawWave.w1e = (int)_extinctionCoefficientWave1.value;
        drawWave.w2e = (int)_extinctionCoefficientWave2.value;
        drawWave.w1n = (int)_refractionIndexWave1.value;
        drawWave.w2n = (int)_refractionIndexWave2.value;

        drawWave.polarizationIndex = _polarizationWave1.value;
        drawWave.polarizationIndex2 = _polarizationWave2.value;

        drawWave.hideComponents = _hideCurvesComponents.isOn;
        drawWave.hideSuperposition = _hideCurvesSuperposition.isOn;
        drawWave.speed = _animationRate.value / 2;

        SetAllText();
    }

    public void SetAllText()
    {
        SetAmplitudeWave1();
        SetWavelengthWave1();
        SetAmplitudeWave2();
        SetWavelengthWave2();
        SetPhaseDifferenceWave2();
        SetLengthMaterial();
        SetExtinctionCoefficientWave1();
        SetRefractionIndexWave1();
        SetExtinctionCoefficientWave2();
        SetRefractionIndexWave2();
        SetAnimationRate();
    }
    private void SetAmplitudeWave1()
    {
        _txtAmplitudeWave1.text = _amplitudeWave1.value.ToString();
    }
    private void SetWavelengthWave1()
    {
        _txtWavelengthWave1.text = _wavelengthWave1.value.ToString();
    }
    private void SetAmplitudeWave2()
    {
        _txtAmplitudeWave2.text = _amplitudeWave2.value.ToString();
    }
    private void SetWavelengthWave2()
    {
        _txtWavelengthWave2.text = _wavelengthWave2.value.ToString();
    }
    private void SetPhaseDifferenceWave2()
    {
        _txtPhaseDifferenceWave2.text = (_phaseDifferenceWave2.value * 10).ToString();
    }
    private void SetLengthMaterial()
    {
        _txtLengthMaterial.text = _lengthMaterial.value.ToString();
    }
    private void SetExtinctionCoefficientWave1()
    {
        _txtExtinctionCoefficientWave1.text = (_extinctionCoefficientWave1.value / 20.0f).ToString("F2");
    }
    private void SetRefractionIndexWave1()
    {
        _txtRefractionIndexWave1.text = (1 + _refractionIndexWave1.value / 20.0f).ToString("F2");
    }
    private void SetExtinctionCoefficientWave2()
    {
        _txtExtinctionCoefficientWave2.text = (_extinctionCoefficientWave2.value / 20.0f).ToString("F2");
    }
    private void SetRefractionIndexWave2()
    {
        _txtRefractionIndexWave2.text = (1 + _refractionIndexWave2.value / 20.0f).ToString("F2");
    }
    private void SetAnimationRate()
    {
        _txtAnimationRate.text = _animationRate.value.ToString();
    }
    public void PolarizationVertical()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 1;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = false;
        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = false;
    }
    public void PolarizationHorizontal()
    {
        _toggleWave2.isOn = true;
        _polarizationWave2.value = 0;
        _amplitudeWave2.value = 5;
        _wavelengthWave2.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave1.isOn = false;
        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = false;
    }
    public void PolarizationLeftCircular()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 2;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = false;
        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = false;
    }
    public void PolarizationRightCircular()
    {
        _toggleWave2.isOn = true;
        _polarizationWave2.value = 3;
        _amplitudeWave2.value = 5;
        _wavelengthWave2.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave1.isOn = false;
        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = false;
    }
    public void LinearAddLinear1()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 1;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = true;
        _polarizationWave2.value = 0;
        _amplitudeWave2.value = 5;
        _wavelengthWave2.value = 4;
        _reverseDirection.isOn = false;

        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = true;
        _phaseDifferenceWave2.value = 0;
    }
    public void LinearAddLinear2()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 1;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = true;
        _polarizationWave2.value = 0;
        _amplitudeWave2.value = 5;
        _wavelengthWave2.value = 4;
        _reverseDirection.isOn = false;

        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = true;
        _phaseDifferenceWave2.value = 9;
    }
    public void LeftAddRightCircular()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 2;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = true;
        _polarizationWave2.value = 3;
        _amplitudeWave2.value = 5;
        _wavelengthWave2.value = 4;
        _reverseDirection.isOn = false;

        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = true;
        _phaseDifferenceWave2.value = 9;
    }
    public void WavelengthRatio1_1()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 1;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = true;
        _polarizationWave2.value = 1;
        _amplitudeWave2.value = 5;
        _wavelengthWave2.value = 4;
        _reverseDirection.isOn = false;

        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = true;
        _phaseDifferenceWave2.value = 0;
    }
    public void WavelengthRatio1_8()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 1;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 1;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = true;
        _polarizationWave2.value = 1;
        _amplitudeWave2.value = 5;
        _wavelengthWave2.value = 8;
        _reverseDirection.isOn = false;

        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = true;
        _phaseDifferenceWave2.value = 0;
    }
    public void WavelengthRatio7_8()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 1;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 7;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = true;
        _polarizationWave2.value = 1;
        _amplitudeWave2.value = 5;
        _wavelengthWave2.value = 8;
        _reverseDirection.isOn = false;

        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = true;
        _phaseDifferenceWave2.value = 0;
    }
    public void StandingWave()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 1;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = true;
        _polarizationWave2.value = 1;
        _amplitudeWave2.value = 5;
        _wavelengthWave2.value = 4;
        _reverseDirection.isOn = true;

        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = true;
        _phaseDifferenceWave2.value = 0;
    }
    public void Material_Absorption()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 1;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = false;
        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = false;
        _lengthMaterial.value = 16;
        _toggleMaterial.isOn = true;
        _extinctionCoefficientWave1.value = 5;
        _refractionIndexWave1.value = 0;
    }
    public void Material_Refraction()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 1;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = false;
        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = false;
        _lengthMaterial.value = 16;
        _toggleMaterial.isOn = true;
        _extinctionCoefficientWave1.value = 0;
        _refractionIndexWave1.value = 10;
    }
    public void Material_Absorption_Refraction()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 1;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = false;
        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = false;

        _lengthMaterial.value = 16;
        _toggleMaterial.isOn = true;
        _extinctionCoefficientWave1.value = 5;
        _refractionIndexWave1.value = 10;
    }
    public void Material_Linear_Dichroism()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 1;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = true;
        _polarizationWave2.value = 0;
        _amplitudeWave2.value = 5;
        _wavelengthWave2.value = 4;
        _reverseDirection.isOn = false;

        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = true;
        _phaseDifferenceWave2.value = 0;

        _lengthMaterial.value = 16;
        _toggleMaterial.isOn = true;
        _extinctionCoefficientWave1.value = 2;
        _refractionIndexWave1.value = 0;
        _extinctionCoefficientWave2.value = 0;
        _refractionIndexWave2.value = 0;

        _sameAsWave1.isOn = false;
    }
    public void Material_Linear_Birefringence()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 1;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = true;
        _polarizationWave2.value = 0;
        _amplitudeWave2.value = 5;
        _wavelengthWave2.value = 4;
        _reverseDirection.isOn = false;

        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = true;
        _phaseDifferenceWave2.value = 0;

        _lengthMaterial.value = 16;
        _toggleMaterial.isOn = true;
        _extinctionCoefficientWave1.value = 0;
        _refractionIndexWave1.value = 1;
        _extinctionCoefficientWave2.value = 0;
        _refractionIndexWave2.value = 0;

        _sameAsWave1.isOn = false;
    }
    public void Material_Linear_Dichroism_Birefringence()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 1;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = true;
        _polarizationWave2.value = 0;
        _amplitudeWave2.value = 5;
        _wavelengthWave2.value = 4;
        _reverseDirection.isOn = false;

        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = true;
        _phaseDifferenceWave2.value = 0;

        _lengthMaterial.value = 16;
        _toggleMaterial.isOn = true;
        _extinctionCoefficientWave1.value = 2;
        _refractionIndexWave1.value = 1;
        _extinctionCoefficientWave2.value = 0;
        _refractionIndexWave2.value = 0;

        _sameAsWave1.isOn = false;
    }
    public void Material_Circular_Dichroism()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 2;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = true;
        _polarizationWave2.value = 3;
        _amplitudeWave2.value = 5;
        _wavelengthWave2.value = 4;
        _reverseDirection.isOn = false;

        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = true;
        _phaseDifferenceWave2.value = 0;

        _lengthMaterial.value = 16;
        _toggleMaterial.isOn = true;
        _extinctionCoefficientWave1.value = 2;
        _refractionIndexWave1.value = 0;
        _extinctionCoefficientWave2.value = 0;
        _refractionIndexWave2.value = 0;

        _sameAsWave1.isOn = false;
    }
    public void Material_Circular_Birefringence()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 2;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = true;
        _polarizationWave2.value = 3;
        _amplitudeWave2.value = 5;
        _wavelengthWave2.value = 4;
        _reverseDirection.isOn = false;

        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = true;
        _phaseDifferenceWave2.value = 0;

        _lengthMaterial.value = 16;
        _toggleMaterial.isOn = true;
        _extinctionCoefficientWave1.value = 0;
        _refractionIndexWave1.value = 1;
        _extinctionCoefficientWave2.value = 0;
        _refractionIndexWave2.value = 0;

        _sameAsWave1.isOn = false;
    }
    public void Material_Circular_Dichroism_Birefringence()
    {
        _toggleWave1.isOn = true;
        _polarizationWave1.value = 2;
        _amplitudeWave1.value = 5;
        _wavelengthWave1.value = 4;
        _reverseDirection.isOn = false;

        _toggleWave2.isOn = true;
        _polarizationWave2.value = 3;
        _amplitudeWave2.value = 5;
        _wavelengthWave2.value = 4;
        _reverseDirection.isOn = false;

        _hideCurvesComponents.isOn = false;
        _hideCurvesSuperposition.isOn = false;
        _toggleWave1AddWave2.isOn = true;
        _phaseDifferenceWave2.value = 0;

        _lengthMaterial.value = 16;
        _toggleMaterial.isOn = true;
        _extinctionCoefficientWave1.value = 2;
        _refractionIndexWave1.value = 1;
        _extinctionCoefficientWave2.value = 0;
        _refractionIndexWave2.value = 0;

        _sameAsWave1.isOn = false;
    }
}
