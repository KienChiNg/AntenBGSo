using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionDropdown : MonoBehaviour
{
    [SerializeField]
    private List<HeaderDropdownPart> _dropdownPart;
    private void Start()
    {
        foreach (HeaderDropdownPart gOPartHeader in _dropdownPart)
            SetUpEventTrigger(gOPartHeader);
    }
    private void SetUpEventTrigger(HeaderDropdownPart gOPartHeader)
    {
        // Lấy hoặc thêm EventTrigger từ GameObject
        EventTrigger eventTrigger = gOPartHeader.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = gOPartHeader.gameObject.AddComponent<EventTrigger>();
        }

        // Tạo một entry mới trong EventTrigger
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter; // Loại sự kiện: PointerClick là khi nhấp chuột

        // Thêm sự kiện vào entry
        entry.callback.AddListener((eventData) => { PointerEnter(gOPartHeader); });

        // Thêm entry vào EventTrigger
        eventTrigger.triggers.Add(entry);

        // Tạo một entry mới trong EventTrigger
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerExit; // Loại sự kiện: PointerClick là khi nhấp chuột

        // Thêm sự kiện vào entry1
        entry1.callback.AddListener((eventData) => { PointerExit(gOPartHeader); });
        // Thêm entry vào EventTrigger
        eventTrigger.triggers.Add(entry1);

    }
    private void PointerEnter(HeaderDropdownPart gOPartHeader)
    {
        // PointerExitAll();
        gOPartHeader.txtPartHeader.color = Const.COLOR_SELECT_CHILD_LESSION;
        gOPartHeader.imgPartHeader.sprite = Resources.Load<Sprite>("Anten/PickHover");
    }
    public void PointerExit(HeaderDropdownPart gOPartHeader)
    {
        gOPartHeader.txtPartHeader.color = Const.COLOR_NORMAL_CHILD_LESSION;
        gOPartHeader.imgPartHeader.sprite = Resources.Load<Sprite>("Anten/PickNormal");
    }
    public void PointerExitAll()
    {
        foreach (HeaderDropdownPart gOPartHeader in _dropdownPart)
        {
            PointerExit(gOPartHeader);
        }
    }
}
