using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LessionChildPart : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _txtPart;
    [SerializeField]
    private GameObject _footerPart;
    public void Selected()
    {
        LessionManager.instance.ResetAllUIPart();
        _txtPart.color = Const.COLOR_SELECT_CHILD_LESSION;
        _footerPart.SetActive(true);
    }
    public void Reset()
    {
        _txtPart.color = Const.COLOR_NORMAL_CHILD_LESSION;
        _footerPart.SetActive(false);
    }
}
