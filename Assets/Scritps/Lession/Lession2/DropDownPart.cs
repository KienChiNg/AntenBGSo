using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropDownPart : MonoBehaviour
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

        // // Tạo một entry mới trong EventTrigger
        // EventTrigger.Entry entry1 = new EventTrigger.Entry();
        // entry1.eventID = EventTriggerType.PointerExit; // Loại sự kiện: PointerClick là khi nhấp chuột

        // // Thêm sự kiện vào entry1
        // entry1.callback.AddListener((eventData) => { PointerExit(gOPartHeader); });
        // eventTrigger.triggers.Add(entry1);
    }
    private void PointerEnter(HeaderDropdownPart gOPartHeader)
    {
        PointerExitAll();
        gOPartHeader.targetChildDropDown.SetActive(true);
    }
    private void PointerExit(HeaderDropdownPart gOPartHeader)
    {
        gOPartHeader.targetChildDropDown.SetActive(false);
    }
    public void PointerExitAll()
    {
        foreach (HeaderDropdownPart gOPartHeader in _dropdownPart)
        {
            PointerExit(gOPartHeader);
        }
    }
}
