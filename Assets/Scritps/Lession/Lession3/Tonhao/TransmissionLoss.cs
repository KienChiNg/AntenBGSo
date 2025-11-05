using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransmissionLoss : MonoBehaviour
{
    public List<LinePos> linePosParentList;
    public List<Toggle> toggleList;
    public Toggle toggleAll;
    public Camera cameraTexRender;
    private void Awake()
    {
        toggleAll.onValueChanged.AddListener(delegate { OnToggleAll(); });
        for (int i = 0; i < toggleList.Count; i++)
        {
            int index = i;
            toggleList[index].onValueChanged.AddListener(delegate { OnValueChanged(index, toggleList[index].isOn); });
        }
    }
    // void OnEnable()
    // {

    // }
    private void Start()
    {
        toggleAll.isOn = true;
    }
    private void OnToggleAll()
    {
        for (int i = 0; i < toggleList.Count; i++)
        {
            int index = i;
            toggleList[index].isOn = toggleAll.isOn;
        }
    }
    private void OnValueChanged(int inx, bool state)
    {
        linePosParentList[inx].gameObject.SetActive(state);

        if (!state) SetDisplayToggleAll(false);
        else
        {
            if (CheckToggleAll())
                SetDisplayToggleAll(true);
        }
    }
    private void SetDisplayToggleAll(bool state)
    {
        toggleAll.onValueChanged.RemoveAllListeners();
        toggleAll.isOn = state;
        toggleAll.onValueChanged.AddListener(delegate { OnToggleAll(); });
    }
    private bool CheckToggleAll()
    {
        foreach (Toggle toggle in toggleList)
        {
            if (!toggle.isOn) return false;
        }
        return true;
    }
    private void OnEnable()
    {
        cameraTexRender.transform.position = new Vector3(0, 22.67469f, -40.33629f);
        cameraTexRender.transform.rotation = Quaternion.Euler(17.287f, 0, 0);
        
        foreach (LinePos linePos in linePosParentList)
        {
            if (linePos != null)
                linePos.enabled = true;
        }
    }
    private void OnDisable()
    {
        foreach (LinePos linePos in linePosParentList)
        {
            if (linePos != null)
                linePos.enabled = false;
        }
    }
}
