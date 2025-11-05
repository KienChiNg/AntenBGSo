using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeaderDropdownPart : MonoBehaviour
{
    public TMP_Text txtPartHeader;
    public Image imgPartHeader;
    public enum ChildDropDown
    {
        None,
        Have
    }
    public ChildDropDown childDropDown;
    [ShowIf("ShowIfChildDropDown")]
    public GameObject targetChildDropDown;
    public bool ShowIfChildDropDown => childDropDown == ChildDropDown.Have;
}
