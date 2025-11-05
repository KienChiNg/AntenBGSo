using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSlider : MonoBehaviour
{
    public int inx;
    public Action<bool> onSelect;
    public Action<bool,int> onSelectInx;
    private bool isSelect;
    public void Hover()
    {
        isSelect = true;
        onSelect?.Invoke(isSelect);
        onSelectInx?.Invoke(isSelect,inx);
        // Debug.Log("KC_Hover");
    }
    public void Normal()
    {
        isSelect = false;
        onSelect?.Invoke(isSelect);
        onSelectInx?.Invoke(isSelect,inx);
        // Debug.Log("KC_Normal");
    }
}
