using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DisplayYesNoMessagePopup : MonoBehaviour
{
    public static DisplayYesNoMessagePopup instance;
    public GameObject prefabs;
    
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {

    }

    public void ShowMessagePopup(string desc, UnityAction okEvent, UnityAction cancelEvent)
    {
        //display popup
        GameObject messageBox = Instantiate(prefabs, GameObject.FindWithTag("MainCanvas").GetComponent<Transform>());
        //find child component
        Text description = messageBox.transform.GetChild(1).GetComponent<Text>();
        Button btnOK = messageBox.transform.GetChild(3).GetComponent<Button>();
        Button btnCancel = messageBox.transform.GetChild(2).GetComponent<Button>();

        //add description
        description.text = desc;
        
        //add btn OK event
        if(okEvent != null)
        {
            btnOK.onClick.AddListener(okEvent);
        }
        btnOK.onClick.AddListener(delegate { Destroy(messageBox); });

        //add btn Cancel event
        if(cancelEvent != null)
        {
            btnCancel.onClick.AddListener(cancelEvent);
        }
        btnCancel.onClick.AddListener(delegate { Destroy(messageBox); });
    }
}
