using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LessionPart : MonoBehaviour
{
    [SerializeField]
    private GameObject _arrow;
    private Image _imgLessionPart;
    [HideInInspector]
    public int inxScene;
    private void Awake()
    {
        _imgLessionPart = GetComponent<Image>();
    }
    private void Start()
    {
        ButtonNormal();
        SetUpEventTrigger();
    }
    private void SetUpEventTrigger()
    {
        // Lấy hoặc thêm EventTrigger từ GameObject
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = gameObject.AddComponent<EventTrigger>();
        }

        // Tạo một entry mới trong EventTrigger
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter; // Loại sự kiện: PointerClick là khi nhấp chuột

        // Thêm sự kiện vào entry
        entry.callback.AddListener((eventData) => { ButtonHover(); });

        // Thêm entry vào EventTrigger
        eventTrigger.triggers.Add(entry);

        // Tạo một entry mới trong EventTrigger
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerExit; // Loại sự kiện: PointerClick là khi nhấp chuột

        // Thêm sự kiện vào entry1
        entry1.callback.AddListener((eventData) => { ButtonNormal(); });

        // Thêm entry1 vào EventTrigger
        eventTrigger.triggers.Add(entry1);

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerDown; // Loại sự kiện: PointerClick là khi nhấp chuột

        // Thêm sự kiện vào entry2
        entry2.callback.AddListener((eventData) => { ButtonDown(); });

        // Thêm entry2 vào EventTrigger
        eventTrigger.triggers.Add(entry2);
    }
    public void ButtonDown()
    {
        SceneManager.LoadScene(inxScene);
    }
    public void ButtonNormal()
    {
        _arrow.SetActive(false);
        _imgLessionPart.sprite = Resources.Load<Sprite>("Home/LessionNormal");
    }
    public void ButtonHover()
    {
        _arrow.SetActive(true);
        _imgLessionPart.sprite = Resources.Load<Sprite>("Home/LessionHover");
    }
}
